import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-stock-location',
  templateUrl: './stock-location.component.html',
  styleUrls: ['./stock-location.component.scss']
})
export class StockLocationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  close() {
    window.history.back();
  }

}
