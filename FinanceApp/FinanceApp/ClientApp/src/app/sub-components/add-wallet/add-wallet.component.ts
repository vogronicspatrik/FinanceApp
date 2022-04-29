import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Wallet} from "../../models/Wallet";
import {CurrencyType} from "../../models/Currency";
import {NgForm} from "@angular/forms";
import {WalletService} from "../../services/wallet.service";

@Component({
  selector: 'app-add-wallet',
  templateUrl: './add-wallet.component.html',
  styleUrls: ['./add-wallet.component.css']
})
export class AddWalletComponent implements OnInit {
  @ViewChild('walletForm', {static: false}) form: any;
  @Output() addNewWallet = new EventEmitter<Wallet>();

  newWallet: Wallet = new Wallet('', '', '', '', '', 0, CurrencyType.EUR, false);
  newWalletOpen: boolean = false;
  currencyTypes = CurrencyType

  constructor(private walletService: WalletService) {
  }

  ngOnInit(): void {
  }

  onSubmit(walletForm: NgForm) {
    this.walletService.addWallet(this.newWallet).subscribe(wallet => {
      wallet.transactions = [];
      this.addNewWallet.emit(wallet);
      walletForm.resetForm();
      this.newWallet = new Wallet('', '', '', '', '', 0, CurrencyType.EUR, false);
    });
  }
}
