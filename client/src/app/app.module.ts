import { Input, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { TestErrorComponent } from './test-error/test-error.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { View1Component } from './development/view1/view1.component';
import { ObservablesComponent } from './development/observables/observables.component';
import { MainPageComponent } from './development/main-page/main-page.component';
import { PonyComponent } from './development/view1/pony/pony.component';
import { TabDirective } from './development/view1/directives/tab.directive';
import { TabsComponent } from './development/view1/tabs/tabs/tabs.component';
import { TabItemComponent } from './development/view1/tabs/tab-item/tab-item.component';
import { StubComponent } from './development/view1/stub/stub.component';

@NgModule({
  declarations: [
    AppComponent,
    TestErrorComponent,
    View1Component,
    ObservablesComponent,
    MainPageComponent,
    PonyComponent,
    TabDirective,
    TabsComponent,
    TabItemComponent,
    StubComponent
  ],
  exports: [ TestErrorComponent ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    NgxSpinnerModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {

  @Input() input = '';

 }
