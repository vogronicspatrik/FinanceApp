import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {CurrencyType} from "../../models/Currency";
import {NgForm} from "@angular/forms";
import {CashFlowDirection, CashFlowItem} from "../../models/CashFlowItem";
import {RecurrencyPeriodType} from "../../models/RecurrencyPeriod";
import {CashFlowItemService} from "../../services/cash-flow-item.service";
import {Loan} from "../../models/Loan";
import {LoanService} from "../../services/loan.service";

@Component({
  selector: 'app-add-cash-flow-item',
  templateUrl: './add-cash-flow-item.component.html',
  styleUrls: ['./add-cash-flow-item.component.css']
})
export class AddCashFlowItemComponent implements OnInit {
  @ViewChild('cashFlowItemForm', {static: false}) form: any;
  @Output() updateItems = new EventEmitter<CashFlowItem>();
  @Output() updateLoans = new EventEmitter<Loan>();

  newItemType: string = 'Regular income'
  newCashFlowItem: CashFlowItem = new CashFlowItem('', '', '', CashFlowDirection.Income, 0, CurrencyType.EUR, RecurrencyPeriodType.Month1);

  loanTermInMonths: number = 120;
  loanInterestRate: number = 5;

  newCashFlowItemOpen: boolean = false;
  currencyTypes = CurrencyType;
  recurrencyPeriods = RecurrencyPeriodType;

  constructor(private cashFlowItemService: CashFlowItemService,
              private loanService: LoanService) {
  }

  ngOnInit(): void {
  }

  onSubmit(cashFlowItemForm: NgForm) {
    if (this.newItemType == 'Regular income') {
      this.newCashFlowItem.type = CashFlowDirection.Income;
      this.addCashFlowItem(cashFlowItemForm);
    } else if (this.newItemType == 'Fix cost') {
      this.newCashFlowItem.type = CashFlowDirection.Cost;
      this.addCashFlowItem(cashFlowItemForm);
    } else {
      let loan: Loan = new Loan('', this.newCashFlowItem.name, this.newCashFlowItem.otherParty, this.newCashFlowItem.amount, this.newCashFlowItem.currency, this.loanTermInMonths, this.loanInterestRate);
      this.loanService.addLoan(loan).subscribe(loan => {
        this.updateLoans.emit(loan);
        cashFlowItemForm.resetForm();
        this.newCashFlowItem = new CashFlowItem('', '', '', CashFlowDirection.Income, 0, CurrencyType.EUR, RecurrencyPeriodType.Month1);
        this.loanTermInMonths = 120;
        this.loanInterestRate = 5;
      });
    }
  }

  addCashFlowItem(cashFlowItemForm: NgForm) {
    this.cashFlowItemService.addCashFlowItem(this.newCashFlowItem).subscribe(cashFlowItem => {
      this.updateItems.emit(cashFlowItem);
      cashFlowItemForm.resetForm();
      this.newCashFlowItem = new CashFlowItem('', '', '', CashFlowDirection.Income, 0, CurrencyType.EUR, RecurrencyPeriodType.Month1);
      this.newItemType = 'Regular income'
    });
  }

  getOtherPartyByType(type: string): string {
    switch (type) {
      case 'Regular income':
        return 'Payment comes from'
      case 'Fix cost':
        return 'Payment goes to'
      case 'Loan':
        return 'Creditor'
      default:
        return ''
    }
  }

  interestRateFix(event: any) {
    const value = Number(event.target.value);
    if (value < 0) {
      this.loanInterestRate = 0;
    } else if (value > 40) {
      this.loanInterestRate = 40;
    }
  }


}
