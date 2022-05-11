import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Currency} from "../../models/Currency";
import {CashFlowItem} from "../../models/CashFlowItem";
import {CashFlowItemService} from "../../services/cash-flow-item.service";

@Component({
  selector: 'app-cash-flow-item',
  templateUrl: './cash-flow-item.component.html',
  styleUrls: ['./cash-flow-item.component.css']
})
export class CashFlowItemComponent implements OnInit {
  @Input() cashFlowItem!: CashFlowItem;
  @Output() deleteItem = new EventEmitter<string>();
  displayValueWithCurrency = Currency.getCurrency;

  constructor(private cashFlowItemService: CashFlowItemService) {
  }

  ngOnInit(): void {
  }

  delete() {
    this.cashFlowItemService.deleteCashFlowItem(this.cashFlowItem.id).subscribe(any => {
      this.deleteItem.emit(this.cashFlowItem.id);
    });
  }

}
