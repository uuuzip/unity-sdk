﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
* @author Taj Santiago
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.Utilities;
using IBM.Watson.Widgets.Question.Facet.FacetElement;

namespace IBM.Watson.Widgets.Question.Facet
{
	public class Features : Base
	{
		[SerializeField]
		private GameObject m_FeatureItemPrefab;

		[SerializeField]
		private RectTransform m_FeaturesCanvasRectTransform;

		private List<FeatureItem> m_FeatureItems = new List<FeatureItem>();

		/// <summary>
		/// Fired when Answer Data is set. Creates Feature Items from prefab reference and adds them to m_featureItems List.
		/// </summary>
		override protected void OnAnswerData()
		{
			for(int i = 0; i < m_Answers.answers[0].features.Length; i++)
			{
				GameObject featureItemGameObject = Instantiate(m_FeatureItemPrefab, new Vector3(95f, -i*50f - 150f, 0f), Quaternion.identity) as GameObject;
				RectTransform featureItemRectTransform = featureItemGameObject.GetComponent<RectTransform>();
				featureItemRectTransform.SetParent(m_FeaturesCanvasRectTransform, false);
				FeatureItem featureItem = featureItemGameObject.GetComponent<FeatureItem>();
				m_FeatureItems.Add(featureItem);
				featureItem.m_Feature = m_Answers.answers[0].features[i].displayLabel;
				featureItem.m_FeatureIndex = m_Answers.answers[0].features[i].weightedScore;
			}
		}

		/// <summary>
		/// Clears dynamically generated Facet Elements when a question is answered. Called from Question Widget.
		/// </summary>
		override public void Clear()
		{
			while(m_FeatureItems.Count != 0) {
				Destroy(m_FeatureItems[0].gameObject);
				m_FeatureItems.Remove(m_FeatureItems[0]);
			}
		}
	}
}