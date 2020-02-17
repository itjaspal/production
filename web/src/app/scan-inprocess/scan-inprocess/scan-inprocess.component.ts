import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { MessageService } from '../../_service/message.service';
import { JobinprocessService } from '../../_service/job-inprocess.service';

@Component({
  selector: 'app-scan-inprocess',
  templateUrl: './scan-inprocess.component.html',
  styleUrls: ['./scan-inprocess.component.scss']
})
export class ScanInprocessComponent implements OnInit {

  constructor(
    private _inproceeSvc: JobinprocessService,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    //private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  ngOnInit() {
  }

  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/home');
  }

}
