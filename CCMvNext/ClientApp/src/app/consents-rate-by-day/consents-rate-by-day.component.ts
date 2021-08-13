import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import { CookieConsentsReportingService } from '../../generated/api';

@Component({
  selector: 'app-consents-rate-by-day',
  templateUrl: './consents-rate-by-day.component.html',
  styleUrls: ['./consents-rate-by-day.component.less']
})
export class ConsentsRateByDayComponent implements OnInit {
  range = new FormGroup({
    start: new FormControl(new Date('2021/8/1')), 
    end: new FormControl(new Date())
  });

  public lineChartData: ChartDataSets[] = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
  ];
  public lineChartLabels: Label[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public lineChartOptions: ChartOptions = {
    responsive: true,
    scales: {
      yAxes: [{
        ticks: { suggestedMin: 0, suggestedMax: 100, stepSize: 20 }
      }]
    }
  };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(255,0,0,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  constructor(private reportingService: CookieConsentsReportingService) { }

  async ngOnInit(): Promise<void>{
    await this.fetchData();
  }

  async fetchData(): Promise<void> {
    const chartData = await this.reportingService.getConsentRate(this.range.value['start'].toJSON(), this.range.value['end'].toJSON()); // ToDo: cast this.range.value to a model.
    const labels: string[] = [];
    const data: number[] = [];

    chartData.forEach(x => {
      labels.push(new Date(x.date).toLocaleDateString("en-US"));
      data.push(x.concentRatePercent);
    });

    this.lineChartData = [
      { data: data, label: 'Consent Rate' },
    ];

    this.lineChartLabels = labels;
  }
}
