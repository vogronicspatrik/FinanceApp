import {Component, OnInit} from '@angular/core';
import {BondService} from "../services/bond.service";
import {Bond, BondReturnInterval} from "../models/Bond";
import {CurrencyType} from "../models/Currency";

@Component({
  selector: 'app-bonds-dashboard',
  templateUrl: './bonds-dashboard.component.html',
  styleUrls: ['./bonds-dashboard.component.css']
})
export class BondsDashboardComponent implements OnInit {
  bonds: Bond[] = [];
  summary: { currency: CurrencyType; intervals: { interval: BondReturnInterval, summary: number }[] }[] = [];

  constructor(private bondService: BondService) {
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.bondService.getBonds().subscribe(bonds => {
      this.bonds = bonds;
    });

    this.bondService.getSummary().subscribe(summary => {
      summary.forEach(item => {
        if (this.summary.length == 0 || this.summary[this.summary.length - 1].currency != item.currency) {
          this.summary.push({currency: item.currency, intervals: []});
        }
        this.summary[this.summary.length - 1].intervals.push({interval: item.interval, summary: item.summary})
      });
    });
  }

  updateBonds($event: Bond) {
    this.load();
  }
}
