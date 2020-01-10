import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pcs-detail',
  templateUrl: './pcs-detail.component.html',
  styleUrls: ['./pcs-detail.component.scss']
})
export class PcsDetailComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  close() {
    window.history.back();
  }


}
