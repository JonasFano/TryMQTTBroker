﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using Microsoft.VisualBasic;
using MQTTnet;
using MQTTnet.Server;
using System.Text;


var mqttFactory = new MqttFactory();

var mqttServerOptions = mqttFactory.CreateServerOptionsBuilder().WithDefaultEndpoint().Build();

var server = mqttFactory.CreateMqttServer(mqttServerOptions);

server.ClientConnectedAsync += e =>
{
    Console.WriteLine("Client Connected");
    return Task.CompletedTask;
};

server.LoadingRetainedMessageAsync += e =>
{
    Console.WriteLine("got msg");
    return Task.CompletedTask;
};


await server.StartAsync();

Console.WriteLine("Server started");
Console.ReadLine();
