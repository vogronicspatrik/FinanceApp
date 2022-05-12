import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Wallet} from "../models/Wallet";
import {CurrencyType} from "../models/Currency";
import {FormControl, FormGroup, NgForm} from "@angular/forms";
import {WalletService} from "../services/wallet.service";

@Component({
  selector: 'app-wallet-transactions',
  templateUrl: './wallet-transactions.component.html',
  styleUrls: ['./wallet-transactions.component.css']
})
export class WalletTransactionsComponent implements OnInit {
  wallet: Wallet = new Wallet('', '', '', '', '', 0, CurrencyType.EUR, false);
  loadedWallet: Wallet = new Wallet('', '', '', '', '', 0, CurrencyType.EUR, false);
  isEdit: boolean = false;

  filterDateEnd: string = "";
  filterDateStart: string = "";

  range = new FormGroup({
    start: new FormControl(),
    end: new FormControl(),
  });

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private walletService: WalletService
  ) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id') ?? "";

    const date = new Date();
    this.filterDateEnd = date.toISOString();
    date.setDate(1);
    this.filterDateStart = date.toISOString();

    this.walletService.getWallet(id, this.filterDateStart, this.filterDateEnd).subscribe(wallet => {
        this.wallet = wallet;
        this.loadedWallet = new Wallet('', this.wallet.location, this.wallet.type, this.wallet.owner, this.wallet.accountNumber, this.wallet.balance, this.wallet.currency, this.wallet.synced);
      },
      err => this.router.navigate(['wallets']));
  }

  onSubmit(walletForm: NgForm) {
    this.walletService.updateWallet(this.wallet.id, this.loadedWallet).subscribe(wallet => {
      this.updateWallet();
      this.unloadUpdate();
    });
  }

  loadUpdate() {
    this.loadedWallet = new Wallet('', this.wallet.location, this.wallet.type, this.wallet.owner, this.wallet.accountNumber, this.wallet.balance, this.wallet.currency, this.wallet.synced);
    this.isEdit = true;
  }

  unloadUpdate() {
    this.loadedWallet = new Wallet('', this.wallet.location, this.wallet.type, this.wallet.owner, this.wallet.accountNumber, this.wallet.balance, this.wallet.currency, this.wallet.synced);
    this.isEdit = false;
  }

  updateWallet() {
    const start = new Date(this.filterDateStart).toISOString();
    const end = new Date(this.filterDateEnd).toISOString();

    this.walletService.getWallet(this.wallet.id, start, end).subscribe(wallet => {
      this.wallet = wallet;
      this.loadedWallet = new Wallet('', this.wallet.location, this.wallet.type, this.wallet.owner, this.wallet.accountNumber, this.wallet.balance, this.wallet.currency, this.wallet.synced);
    });
  }

  deleteWallet() {
    if (confirm("Are you sure you want to delete the wallet?")) {
      this.walletService.deleteWallet(this.wallet.id).subscribe(any => {
        this.router.navigate(['wallets']);
      });
    }
  }

  deleteTransaction(id: string) {
    this.walletService.deleteTransaction(id).subscribe(any => {
      this.updateWallet();
    });
  }
}
