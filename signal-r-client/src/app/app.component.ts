import { SignalRService } from './services/signal-r.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private signalR: SignalRService){}

  async connect() {
    await this.signalR.connect();
    this.signalR.addListener();
    this.signalR.data.subscribe(data => {
      console.log(data);
    })
  }

  
}
