import {Component, Input, OnInit} from '@angular/core';
import {Wallet} from "../../models/Wallet";

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.css']
})
export class WalletComponent implements OnInit {
  @Input() wallet!: Wallet;
  open: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }
}
