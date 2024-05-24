import { Component, NgModule } from '@angular/core';
import { RouterOutlet,RouterModule,RouterLink } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HomeComponent,NavbarComponent,RouterOutlet,RouterModule,RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})


export class AppComponent {
  title = 'FMA-ANGULAR';
}
