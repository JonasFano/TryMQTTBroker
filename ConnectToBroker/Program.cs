// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using MQTTnet;
using MQTTnet.Client;

var mqttFactory = new MqttFactory();

var mqttClient = mqttFactory.CreateMqttClient();
// Use builder classes where possible in this project.
var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("192.168.178.55").Build();

// This will throw an exception if the server is not available.
// The result from this message returns additional data which was sent 
// from the server. Please refer to the MQTT protocol specification for details.
var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

Console.WriteLine("The MQTT client is connected.");
Console.WriteLine(response.ResultCode.ToString());

var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic("samples/temperature/living_room")
            .WithPayload("Ich liebe dich Kai #NoHomo")
.Build();

await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

await mqttClient.DisconnectAsync();

Console.WriteLine("MQTT application message is published.");



// Send a clean disconnect to the server by calling DisconnectAsync. Without this the TCP connection
// gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);




