import { AuthenticationService } from './../../../_service/authentication.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'my-app-sidenav-menu',
  styles: [],
  templateUrl: './sidenav-menu.component.html'
})

export class AppSidenavMenuComponent {

  public user: any;
  public allowDashboard: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
  ) { }

  async ngOnInit() {
    this.user = this.authenticationService.getLoginUser();
    
    let userMenuGroup = this.user.menuGroups;
    this.user.menuGroups = userMenuGroup.filter(x => x.menuFunctionGroupId != '01');

    let dashMenu = userMenuGroup.filter(x => x.menuFunctionGroupId == '01');
    this.allowDashboard = dashMenu.length > 0;
  }
}
