import { Directive, OnChanges, Input, TemplateRef, ViewContainerRef, SimpleChanges } from '@angular/core';
import { Role } from '@core/enums/role';
import { AuthService } from '@core/services/auth.service';

@Directive({
  selector: '[rbac]'
})
export class RoleBasedAccessControlDirective implements OnChanges {
  private requiredRoles: Role[] = [];
  private visible: boolean = false;

  @Input() set rbac(roles: Role[]) {
    this.requiredRoles = roles;
  }

  constructor(
    private authService: AuthService,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.validateAccess();
  }

  private validateAccess() {
    if (this.visible)
      return;

    const hasAccess = this.authService.haveAccessToAnyRoles(this.requiredRoles);

    if (hasAccess) {
      this.viewContainer.clear();
      this.viewContainer.createEmbeddedView(this.templateRef);
      this.visible = true;
    } else {
      this.viewContainer.clear();
      this.visible = false;
    }
  }
}
