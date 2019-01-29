//
// API.AI Unity SDK Sample
// =================================================
//
// Copyright (C) 2015 by Speaktoit, Inc. (https://www.speaktoit.com)
// https://www.api.ai
//
// ***********************************************************************************************************************
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// ***********************************************************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Reflection;
using ApiAiSDK;
using ApiAiSDK.Model;
using ApiAiSDK.Unity;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

public class Parameters
{
    public string date { get; set; }
}

public class Metadata
{
    public string intentName { get; set; }
    public string intentId { get; set; }
}

public class Fulfillment
{
    public string speech { get; set; }
}

public class Result
{
    public string action { get; set; }
    public Parameters parameters { get; set; }
    public List<object> contexts { get; set; }
    public Metadata metadata { get; set; }
    public string resolvedQuery { get; set; }
    public Fulfillment fulfillment { get; set; }
    public string source { get; set; }
}

public class Status
{
    public int code { get; set; }
    public string errorType { get; set; }
}

public class Resultado
{
    public string id { get; set; }
    public DateTime timestamp { get; set; }
    public Result result { get; set; }
    public Status status { get; set; }
    public bool IsError { get; set; }
}

public class ApiAiModuleMod : MonoBehaviour
{

    private ApiAiUnity apiAiUnity;
  

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    { 
        NullValueHandling = NullValueHandling.Ignore,
    };


    // Use this for initialization
    void Start()
    {
        // check access to the Microphone
        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) =>
        {
            return true;
        };
        const string ACCESS_TOKEN = "6d22268c2d7b419fa919bd347df35a38";

        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.Spanish);
        
        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);      

        apiAiUnity.OnResult += HandleOnResult;
        apiAiUnity.OnError += HandleOnError;

    }

    // Update is called once per frame
    void Update()
    {
        if (apiAiUnity != null)
        {
            apiAiUnity.Update();
        }
    }

    public void StartListening()
{
    try {
        var aud = GetComponent<AudioSource>();
        apiAiUnity.StartListening(aud);
        
    } catch (Exception ex) {
        Debug.LogException(ex);
    }
    StartCoroutine(StopListening());
}

    public IEnumerator  StopListening()
    {
        yield return new WaitForSeconds(4.0f);
        try
        {
            apiAiUnity.StopListening();
        } 
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }



    public void StartNativeRecognition()
    {
        try
        {
            
            apiAiUnity.StartNativeRecognition();
            GameController.controlador.ShowHidePanelMacListening();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


    
    public string SendText(String asking)
    {
        var text = asking;

        Debug.Log(text);
        
        AIResponse response = apiAiUnity.TextRequest(text);
        
        if (response != null)
        {

            Debug.Log("Result: " + response);
            return  response.Result.Fulfillment.Speech;

        } else
        {
            Debug.LogError("Response is null");
            return "Error";
        }

    }

    void HandleOnResult(object sender, AIResponseEventArgs e)
    {
        var aiResponse = e.Response;
        Debug.Log("Resultado");
        GameController.controlador.ShowHidePanelMacListening();

        if (aiResponse != null) {
            Debug.Log(aiResponse.Result.Fulfillment.Speech);
            GameController.controlador.TextToSpeakFunc(aiResponse.Result.Fulfillment.Speech);
        } else {
            Debug.LogError("Response is null");
            GameController.controlador.TextToSpeakFunc("Lo siento no se en que puedo ayudarte");
        }
    }

    void HandleOnError(object sender, AIErrorEventArgs e)
    {
        GameController.controlador.ShowHidePanelMacListening();
        GameController.controlador.ShowFrontPanel("Error");
        Debug.LogException(e.Exception);
    }

}
