import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Loan} from "../../models/Loan";
import {LoanService} from "../../services/loan.service";

@Component({
  selector: 'app-loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.css']
})
export class LoanComponent implements OnInit {
  @Input() loan!: Loan;
  @Output() deleteLoan = new EventEmitter<string>();
  open: boolean = false;

  constructor(private loanService: LoanService) {
  }

  ngOnInit(): void {
  }

  pay() {
    this.loanService.payInstallment(this.loan.id).subscribe(loan => {
      this.loan = loan;
    });
  }

  delete() {
    this.loanService.deleteLoan(this.loan.id).subscribe(any => {
      this.deleteLoan.emit(this.loan.id);
    });
  }
}
