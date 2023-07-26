// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using MQTTnet;
using MQTTnet.Server;
using System.Text;


class BrokerServer {

    private MqttServer mqttServer;

    private string payload;

    public string Payload
    {
        get { return payload; }
    }

    public BrokerServer()
    {
        payload = "";

        var mqttFactory = new MqttFactory();

        var mqttServerOptions = mqttFactory.CreateServerOptionsBuilder()
            .WithDefaultEndpoint()
            .Build();

        mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);

        mqttServer.ClientConnectedAsync += ClientConnect;

        mqttServer.InterceptingPublishAsync += RecMSG;

        mqttServer.StartAsync();
        Console.WriteLine("Server Started");
    }

    private Task ClientConnect(ClientConnectedEventArgs args)
    {
        Console.WriteLine("Client Connected");
        return Task.CompletedTask;
    }

    private Task RecMSG(InterceptingPublishEventArgs args)
    {
        payload = Encoding.UTF8.GetString(args.ApplicationMessage?.Payload);
        Console.WriteLine("msg: " + payload);
        return Task.CompletedTask;
    }
    
    public void Close()
    {
        var task = mqttServer.StopAsync();
        task.Wait();
        Console.WriteLine("Close Server");
    }

}

