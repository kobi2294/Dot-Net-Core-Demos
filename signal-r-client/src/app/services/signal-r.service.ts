import { ChartModel } from './../models/chart-model';
import { Injectable } from '@angular/core';
import * as SignalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: ChartModel[];
  private hubConnection: SignalR.HubConnection;

  constructor() { }

  public connect() {
    this.hubConnection = new SignalR.HubConnectionBuilder()
                            .withUrl('http://localhost:5001/chart')
                            .build();
    this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while creating connection: ' + err))
  }

  public addListener() {
    this.hubConnection.on('chart-data', (data) => {
      this.data = data;
      console.log(data);
    });
  }
}
