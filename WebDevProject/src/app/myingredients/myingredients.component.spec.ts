import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyingredientsComponent } from './myingredients.component';

describe('MyingredientsComponent', () => {
  let component: MyingredientsComponent;
  let fixture: ComponentFixture<MyingredientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyingredientsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyingredientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
