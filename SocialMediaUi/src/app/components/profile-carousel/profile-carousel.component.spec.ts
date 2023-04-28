import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileCarouselComponent } from './profile-carousel.component';

describe('ProfileCarouselComponent', () => {
  let component: ProfileCarouselComponent;
  let fixture: ComponentFixture<ProfileCarouselComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileCarouselComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileCarouselComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
