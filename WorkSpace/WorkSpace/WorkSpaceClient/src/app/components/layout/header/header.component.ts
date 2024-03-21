import { Component, HostListener, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';

import { IResponse } from 'src/app/models/shared/response';
import { AuthService } from 'src/app/services/auth.service';



@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})

export class HeaderComponent implements OnInit {
  userRole: string | null = '3';
  routingUrl!: string;
  links!: Array<{ text: string; icon: string; route: string }>;
  activeLinkIndex: number = 0;
  userName: string | null = '';
  isIconContainerVisible = true;
  isMobileScreen = false;
  mobileScreenWidth = 800;
  constructor(
    private router: Router,
    private authService: AuthService,
  ) {

  }

  ngOnInit(): void {
    this.userName = this.authService.getUserName();
  }
  setActiveLink(index: number) {
    this.activeLinkIndex = index;
  }

  setActiveLinkBasedOnRoute(url: string) {
    for (let i = 0; i < this.links?.length; i++) {
      if (this.links[i].route && url.includes(this.links[i].route)) {
        this.activeLinkIndex = i;
        break;
      }
    }
  }
  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.checkScreenWidth();
  }
  toggleIconContainer() {
    if (this.isMobileScreen) {
      this.isIconContainerVisible = !this.isIconContainerVisible;
    }
  }
  checkScreenWidth() {
    this.isMobileScreen = window.innerWidth <= this.mobileScreenWidth;
    this.isIconContainerVisible = !this.isMobileScreen;
  }
  logout() {
    this.authService.logout();
  }
}
