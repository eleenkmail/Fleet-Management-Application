import { Component, OnInit } from '@angular/core';
import { RouterOutlet,RouterModule,RouterLink } from '@angular/router';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule,RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{
  constructor() { }
ngOnInit(): void {


}
}
