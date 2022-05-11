import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {ValuableItemService} from "../../services/valuable-item.service";
import {ValuableItem} from "../../models/ValuableItem";
import {CurrencyType} from "../../models/Currency";

@Component({
  selector: 'app-add-valuable-item',
  templateUrl: './add-valuable-item.component.html',
  styleUrls: ['./add-valuable-item.component.css']
})
export class AddValuableItemComponent implements OnInit {
  @ViewChild('valuableItemForm', {static: false}) form: any;
  @Output() updateItems = new EventEmitter<ValuableItem>();

  newValuableItem: ValuableItem = new ValuableItem('', '', 0, CurrencyType.EUR, '', 0);
  newValuableItemOpen: boolean = false;
  currencyTypes = CurrencyType

  constructor(private valuableItemService: ValuableItemService) {
  }

  ngOnInit(): void {
    this.setDate();
  }

  onSubmit(valuableItemForm: NgForm) {
    this.valuableItemService.addValuableItem(this.newValuableItem).subscribe(valuableItem => {
      this.updateItems.emit(valuableItem);
      valuableItemForm.resetForm();
      this.newValuableItem = new ValuableItem('', '', 0, CurrencyType.EUR, '', 0);
      this.setDate();
    });
  }

  setDate() {
    const date = new Date();
    date.setSeconds(0);
    date.setMinutes(0);
    date.setHours(0);
    this.newValuableItem.dateOfPurchasing = date.toISOString();
  }

  percentageFix(event: any) {
    const value = Number(event.target.value);
    if (value < 0) {
      this.newValuableItem.amortizationRatePerYear = 0;
    } else if (value > 100) {
      this.newValuableItem.amortizationRatePerYear = 100;
    }
  }
}
