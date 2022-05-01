import {Component, OnInit} from '@angular/core';
import {Wallet} from "../models/Wallet";
import {CurrencyType} from "../models/Currency";
import {Transaction} from "../models/Transaction";
import {WalletService} from "../services/wallet.service";

@Component({
  selector: 'app-wallet-dashboard',
  templateUrl: './wallet-dashboard.component.html',
  styleUrls: ['./wallet-dashboard.component.css']
})
export class WalletDashboardComponent implements OnInit {
  wallets: Wallet[] = [];

  constructor(private walletService: WalletService) {
  }

  ngOnInit(): void {
    this.walletService.getWallets().subscribe(wallets => {
      this.wallets = wallets;
    });
  }

  addNewWallet($event: any) {
    this.wallets.push($event);
  }
}
