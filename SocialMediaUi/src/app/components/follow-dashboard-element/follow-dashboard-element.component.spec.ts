import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FollowDashboardElementComponent } from './follow-dashboard-element.component';

describe('FollowDashboardElementComponent', () => {
  let component: FollowDashboardElementComponent;
  let fixture: ComponentFixture<FollowDashboardElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FollowDashboardElementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FollowDashboardElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
