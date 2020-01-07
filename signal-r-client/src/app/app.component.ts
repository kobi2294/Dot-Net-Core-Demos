import { ChartModel } from './models/chart-model';
import { SignalRService } from './services/signal-r.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  data$: Observable<ChartModel[]>;

  chartOptions: any = {
  }

  constructor(private signalR: SignalRService){    
  }

  async ngOnInit() {
    await this.signalR.connect();
    this.signalR.addListener();
    this.data$ = this.signalR.data;
  }
  
}
