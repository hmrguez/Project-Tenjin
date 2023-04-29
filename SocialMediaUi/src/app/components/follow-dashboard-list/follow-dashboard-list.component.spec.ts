import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowDashboardListComponent } from './follow-dashboard-list.component';

describe('FollowDashboardListComponent', () => {
  let component: FollowDashboardListComponent;
  let fixture: ComponentFixture<FollowDashboardListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FollowDashboardListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FollowDashboardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
