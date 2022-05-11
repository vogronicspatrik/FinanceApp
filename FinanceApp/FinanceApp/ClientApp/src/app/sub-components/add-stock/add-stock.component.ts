import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {Stock} from "../../models/Stock";
import {CurrencyType} from "../../models/Currency";
import {NgForm} from "@angular/forms";
import {StockService} from "../../services/stock.service";
import {AlphaService} from 'src/app/services/alpha.service';

@Component({
  selector: 'app-add-stock',
  templateUrl: './add-stock.component.html',
  styleUrls: ['./add-stock.component.css']
})
export class AddStockComponent implements OnInit {
  @ViewChild('stockForm', {static: false}) form: any;
  @Output() addNewStock = new EventEmitter<Stock>();

  newStock: Stock = new Stock('', '', '', '', CurrencyType.EUR, 0, 0);
  newStockOpen: boolean = false;
  currencyTypes = CurrencyType;

  symbols: string[] = [];

  constructor(private stockService: StockService, private alphaService: AlphaService) {
  }

  ngOnInit(): void {
  }

  onSubmit(stockForm: NgForm) {
    this.stockService.addStock(this.newStock).subscribe(stock => {
      this.addNewStock.emit(stock);
      stockForm.resetForm();
      this.newStock = new Stock('', '', '', '', CurrencyType.EUR, 0, 0);
    });
  }

  getSymbols($event: any) {
    console.log($event.target.value + $event.key);
    this.alphaService.getSymbols($event.target.value + $event.key).subscribe(symbols => {
      this.symbols = symbols;
      console.log(this.symbols);
    })
  }
}
