import {Component, Input, OnInit} from '@angular/core';
import {Wallet} from "../../models/Wallet";
import {Currency} from "../../models/Currency";

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.css']
})
export class WalletComponent implements OnInit {
  @Input() wallet!: Wallet;
  open: boolean = false;
  walletGetBalance = Currency.getBalance;
  transactionGetPrice = Currency.getPrice;

  constructor() {
  }

  ngOnInit(): void {
  }
}
