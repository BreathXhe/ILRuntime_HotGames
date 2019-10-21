﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class UICreateAvatar : AHotBase
{
	Button btnReturn;
	InputField inputNickname;
	Button btnMale;
	Transform selMale;
	Button btnFemale;
	Transform selFemale;
	bool bMale
	{
		get
		{
			return selMale.gameObject.activeInHierarchy;
		}
		set
		{
			selMale.gameObject.SetActive(value);
			selFemale.gameObject.SetActive(!value);
		}
	}
	Button btnCreate;
	protected override void InitComponents()
	{
		btnReturn = FindWidget<Button>("btnReturn");
		btnReturn.onClick.AddListener(() =>
		{
			OnUnloadThis();

			URemoteData.OnLogout();
			LoadAnotherUI<UILogin>();
		});

		inputNickname = FindWidget<InputField>("inputNickname");
		inputNickname.text = "";

		btnMale = FindWidget<Button>("btnMale");
		btnMale.onClick.AddListener(() =>
		{
			bMale = true;
		});
		selMale = FindWidget<Transform>(btnMale.transform, "sel");
		btnFemale = FindWidget<Button>("btnFemale");
		btnFemale.onClick.AddListener(() =>
		{
			bMale = false;
		});
		selFemale = FindWidget<Transform>(btnFemale.transform, "sel");
		bMale = true;

		btnCreate = FindWidget<Button>("btnCreate");
		btnCreate.onClick.AddListener(() =>
		{
			var nickname = inputNickname.text;
			if (string.IsNullOrEmpty(nickname))
			{
				return;
			}
			UStaticWebRequests.DoCreateAvatar(UILogin.CachedUsername, UILogin.token, nickname, bMale ? "1" : "0", jsuccess =>
			{
				URemoteData.OnReceiveAvatarData(jsuccess["avatar"].ToString());
				OnUnloadThis();

				LoadAnotherUI<UIMain>();
			});
		});
	}
}

