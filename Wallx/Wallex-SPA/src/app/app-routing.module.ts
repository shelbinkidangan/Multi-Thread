import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PublicLayoutComponent } from './public-layout/public-layout.component';

const routes: Routes = [

  // Main redirect
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '', component: PublicLayoutComponent,
    children: [
      { path: 'home', component: HomeComponent },
    ]
  },

  // Handle all other routes
  { path: '**', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
