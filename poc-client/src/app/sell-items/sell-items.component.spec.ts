import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellItemsComponent } from './sell-items.component';

describe('SellItemsComponent', () => {
  let component: SellItemsComponent;
  let fixture: ComponentFixture<SellItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
