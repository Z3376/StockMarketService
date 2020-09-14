import { Component, OnInit } from '@angular/core';
import { StockPriceDto } from '../../../models/stock-price-dto';

@Component({
  selector: 'app-user-bar-chart',
  templateUrl: './user-bar-chart.component.html',
  styleUrls: ['./user-bar-chart.component.css']
})
export class UserBarChartComponent implements OnInit {
  item : StockPriceDto;
  constructor() { 
    this.item = new StockPriceDto();
  }
  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };
  public barChartLabels = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  public barChartType = 'bar';
  public barChartLegend = false;
  public barChartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A'},
    {data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B'}
  ];
  ngOnInit(): void {
  }
  public GetStockPriceList()
  {
    this.service.GetCompany(this.item).subscribe((item:Company)=>{
      this.item=item[0]
      console.log(this.item)
    },(err)=>{
      console.log(err.error)
    })
  }
}