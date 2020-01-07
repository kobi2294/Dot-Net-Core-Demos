import { ChartModel } from './../models/chart-model';
import { Injectable } from '@angular/core';
import * as SignalR from '@aspnet/signalr';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: SignalR.HubConnection;

  private _data = new BehaviorSubject<ChartModel[]>(null);  
  public get data(): Observable<ChartModel[]>  {
    return this._data.asObservable();
  }

  constructor() { }

  public async connect() : Promise<void> {
    this.hubConnection = new SignalR.HubConnectionBuilder()
                            .withUrl('http://localhost:5001/chart')
                            .build();

    try {
      await this.hubConnection.start();
      console.log('Connection started successfully');  
    }
    catch (err) {
      console.log('Error while creating connection: ' + err);
    }
  }

  public addListener() {
    this.hubConnection.on('chart-data', (data) => {
      this._data.next(data);
    });
  }

  public async randomizeAll() {
    await this.hubConnection.invoke('randomizeall');
  }

  public async randomizeMe() {
    await this.hubConnection.invoke('randomizeme');
  }


}
