import {Component, Input, OnInit} from '@angular/core';
import {CurrencyType} from "../../models/Currency";

@Component({
  selector: 'app-value-with-currency',
  templateUrl: './value-with-currency.component.html',
  styleUrls: ['./value-with-currency.component.css']
})
export class ValueWithCurrencyComponent implements OnInit {
  @Input() value!: number;
  @Input() currency!: CurrencyType;
  @Input() negate: boolean | undefined;

  isSuffix: boolean = false;
  currencySymbol: string = "";

  constructor() {
  }

  ngOnInit(): void {
    this.setCurrencySymbol(this.currency);
  }

  public setCurrencySymbol(currency: CurrencyType) {
    switch (currency) {
      case CurrencyType.EUR:
        this.currencySymbol = "\u20AC";
        break;
      case CurrencyType.HUF:
        this.currencySymbol = "HUF";
        this.isSuffix = true;
        break;
    }
  }

}
