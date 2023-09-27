import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleBasedAccessControlDirective } from './role-based-access-control.directive';

export const sharedDirectives: Array<Type<any>> = [
  RoleBasedAccessControlDirective
];

@NgModule({
  declarations: [
    ...sharedDirectives
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ...sharedDirectives
  ]
})
export class DirectivesModule { }
