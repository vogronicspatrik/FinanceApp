import {Component, OnInit} from '@angular/core';
import * as Highcharts from 'highcharts';
import {WalletService} from "../services/wallet.service";
import {CategoryStatistic} from "../models/CategoryStatistic";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  incomeStatistic: CategoryStatistic[] = [];
  expenseStatistic: CategoryStatistic[] = [];

  incomeEmpty: boolean = false;
  expenseEmpty: boolean = false;

  filterDateEnd: string = "";
  filterDateStart: string = "";

  disableSearch: boolean = false;

  constructor(private walletService: WalletService) {
  }

  ngOnInit(): void {
    const date = new Date();
    this.filterDateEnd = date.toISOString();
    date.setDate(1);
    this.filterDateStart = new Date(date.getFullYear(), date.getMonth() - 1, 1).toISOString();

    this.updateSearch();
  }

  updateSearch() {
    this.disableSearch = true;
    this.incomeEmpty = false;
    this.expenseEmpty = false;
    const start = new Date(this.filterDateStart).toISOString();
    const end = new Date(this.filterDateEnd).toISOString();
    this.walletService.getStatistic(start, end).subscribe(any => {
      this.incomeStatistic = any[0];
      this.expenseStatistic = any[1];
      if (this.incomeStatistic.length == 0) {
        this.incomeEmpty = true;
      } else {
        this.createChartPie(this.incomeStatistic, 'income-chart');
      }

      if (this.expenseStatistic.length == 0) {
        this.expenseEmpty = true;
      } else {
        this.createChartPie(this.expenseStatistic, 'expense-chart');
      }
    });

    setTimeout(() => {
      this.disableSearch = false;
    }, 10000)
  }

  createChartPie(statistic: CategoryStatistic[], chartId: string): void {

    let data: { name: string; y: number; color: string; }[] = [];

    statistic.forEach(item => data.push({
      name: item.categoryName,
      y: item.percentage * 100,
      color: item.categoryColor
    }));

    const chart = Highcharts.chart(chartId, {
      chart: {
        type: 'pie',
      },
      title: {
        text: null,
      },
      credits: {
        enabled: false,
      },
      tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
      },
      plotOptions: {
        pie: {
          allowPointSelect: true,
          cursor: 'pointer',
          dataLabels: {
            enabled: true,
            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
          }
        }
      },
      series: [{
        name: 'Categories',
        colorByPoint: true,
        innerSize: '50%',
        data: data
      }]
    } as any);

    /*setInterval(() => {
      date.setDate(date.getDate() + 1);
      chart.series[0].addPoint({
        name: `${date.getDate()}/${date.getMonth() + 1}`,
        y: this.getRandomNumber(0, 1000),
      }, true, true);
    }, 1500);*/
  }
}
