import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-data',
  template: `
    <p>
      data works!
    </p>
    <app-exchange-rate [apiUrl]="'rates'"></app-exchange-rate>
  `,
  styles: []
})
export class DataComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
