import {Component, OnInit} from '@angular/core';
import {Wallet} from "../models/Wallet";
import {CurrencyType} from "../models/Currency";
import {WalletService} from "../services/wallet.service";

@Component({
  selector: 'app-wallet-dashboard',
  templateUrl: './wallet-dashboard.component.html',
  styleUrls: ['./wallet-dashboard.component.css']
})
export class WalletDashboardComponent implements OnInit {
  wallets: Wallet[] = [];
  summary: { currency: CurrencyType; summary: number }[] = [];

  constructor(private walletService: WalletService) {
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.walletService.getWallets().subscribe(wallets => {
      this.wallets = wallets;
    });
    this.walletService.getSummary().subscribe(summary => {
      this.summary = summary;
    });
  }

  addNewWallet($event: any) {
    this.wallets.push($event);
  }
}
