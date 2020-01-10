import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pcs-history',
  templateUrl: './pcs-history.component.html',
  styleUrls: ['./pcs-history.component.scss']
})
export class PcsHistoryComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  close() {
    window.history.back();
  }

}
