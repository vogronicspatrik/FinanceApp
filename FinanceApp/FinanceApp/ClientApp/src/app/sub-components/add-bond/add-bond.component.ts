import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {ValuableItem} from "../../models/ValuableItem";
import {CurrencyType} from "../../models/Currency";
import {ValuableItemService} from "../../services/valuable-item.service";
import {NgForm} from "@angular/forms";
import {Bond, BondReturnInterval} from "../../models/Bond";
import {BondService} from "../../services/bond.service";

@Component({
  selector: 'app-add-bond',
  templateUrl: './add-bond.component.html',
  styleUrls: ['./add-bond.component.css']
})
export class AddBondComponent implements OnInit {
  @ViewChild('bondForm', {static: false}) form: any;
  @Output() updateBonds = new EventEmitter<Bond>();

  newBond: Bond = new Bond('', '', '', 0, 0, CurrencyType.EUR, '', 1, 0, BondReturnInterval.Year);
  newBondOpen: boolean = false;
  currencyTypes = CurrencyType

  constructor(private bondService: BondService) {
  }

  ngOnInit(): void {
    this.setDate();
  }

  onSubmit(bondForm: NgForm) {
    this.bondService.addBond(this.newBond).subscribe(bond => {
      this.updateBonds.emit(bond);
      bondForm.resetForm();
      this.newBond = new Bond('', '', '', 0, 0, CurrencyType.EUR, '', 1, 0, BondReturnInterval.Year);
      this.setDate();
    });
  }

  setDate() {
    const date = new Date();
    date.setSeconds(0);
    date.setMinutes(0);
    date.setHours(0);
    this.newBond.purchaseDate = date.toISOString();
    this.newBond.maturityDate = date.toISOString();
  }

  percentageFix(event: any) {
    const value = Number(event.target.value);
    if (value < 0) {
      this.newBond.returnRate = 0;
    } else if (value > 60) {
      this.newBond.returnRate = 100;
    }
  }
}
