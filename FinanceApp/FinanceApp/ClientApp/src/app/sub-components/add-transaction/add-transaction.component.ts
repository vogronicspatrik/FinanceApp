import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {Wallet} from "../../models/Wallet";
import {WalletService} from "../../services/wallet.service";
import {NgForm} from "@angular/forms";
import {CategoryService} from "../../services/category.service";
import {Category} from "../../models/Category";
import {Transaction, TransactionType} from "../../models/Transaction";

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {
  @ViewChild('transactionForm', {static: false}) form: any;
  @Output() updateWallet = new EventEmitter<any>();
  @Input() wallet!: Wallet;

  categories: Category[] = [];

  newTransaction: Transaction = new Transaction('', TransactionType.Expense, '', 0, new Category('0', '', ''), "");
  newTransactionOpen: boolean = false;

  constructor(
    private walletService: WalletService,
    private categoryService: CategoryService
  ) {
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories
    });

    this.setDate();
  }

  onSubmit(transactionForm: NgForm) {
    this.walletService.createTransaction(this.newTransaction, this.wallet.id, this.newTransaction.category.id).subscribe(transaction => {
      this.updateWallet.emit();
      transactionForm.resetForm();
      this.newTransaction = new Transaction('', TransactionType.Expense, '', 0, new Category('0', '', ''), "");
      this.setDate();
    });
  }

  setDate() {
    const date = new Date();
    date.setSeconds(0);
    this.newTransaction.dateOfTransaction = date.toISOString();
  }
}
