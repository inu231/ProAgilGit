// Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { AppRoutingModule } from './app-routing.module';

// Componentes
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { HttpClient } from 'selenium-webdriver/http';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

// Eventos
import { EventoService } from './_services/Evento.service';
import { BsDatepickerModule } from 'ngx-bootstrap';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      ModalModule.forRoot(),
      TooltipModule.forRoot(),
      ReactiveFormsModule,
      BsDatepickerModule.forRoot()
   ],
   providers: [
      EventoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
