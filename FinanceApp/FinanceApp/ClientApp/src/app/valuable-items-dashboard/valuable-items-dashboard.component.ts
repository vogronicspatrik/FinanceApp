import {Component, OnInit} from '@angular/core';
import {ValuableItem} from "../models/ValuableItem";
import {ValuableItemService} from "../services/valuable-item.service";
import {Currency, CurrencyType} from "../models/Currency";

@Component({
  selector: 'app-valuable-items-dashboard',
  templateUrl: './valuable-items-dashboard.component.html',
  styleUrls: ['./valuable-items-dashboard.component.css']
})
export class ValuableItemsDashboardComponent implements OnInit {
  valuableItems: ValuableItem[] = [];

  summary: { currency: CurrencyType, withAmortization: number, withoutAmortization: number }[] = [];

  displayValueWithCurrency = Currency.getCurrency;

  constructor(private valuableItemService: ValuableItemService) {
  }

  ngOnInit(): void {
    this.updateItems();
  }

  updateItems() {
    this.valuableItemService.getValuableItems().subscribe(valuableItems => {
      this.valuableItems = valuableItems;
    });

    this.valuableItemService.getSummary().subscribe(summary => {
      this.summary = summary;
    });
  }

}
