import { SharedService } from './../shared/shared.service';
import { AddPizzaToCart } from '../models/AddPizzaToCart';
import { PizzaTypeForOrder } from './../models/pizzaTypeForOrder';
import { SizeForOrder } from './../models/sizeForOrder';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { OrderService } from './order.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
  providers: [OrderService]
})
export class OrderComponent implements OnInit {
  constructor(
    private orderService: OrderService,
    private formBuilder: FormBuilder,
    private router: Router,
    private sharedService: SharedService,
    public dialog: MatDialog
  ) { }

  submitted = false;

  submitForm: FormGroup;

  sizeId: FormControl = new FormControl('', [Validators.required]);
  pizzaTypeId = new FormControl('', Validators.required);

  numberOfPizza = new FormControl('', Validators.required);
  sizesForOrder: SizeForOrder[] = [];
  pizzaTypesForOrder: PizzaTypeForOrder[] = [];
  public selectedPizzaTypesForOrder: PizzaTypeForOrder[] = [];
  toppingsForOrder: string[] = [];
  selectedToppings: string[] = [];
  pizzaToAddCart: AddPizzaToCart = new AddPizzaToCart();

  edgeTypeId = new FormControl(0);
  pizzaCategoryId = new FormControl('0');

  selectedPizzaTypeId: number = 0;
  selectedPizzaSizeId: number = 0;
  public selectedPizzaNumber: number = 0;
  selectedToppingsNumber: number = 0;
  selectedToppingsPrice: number = 0;
  selectedPizzaPrice: number = 0;
  selectedPizzaCategoryeId: number = 0;

  totalPrice : number =0;

  ngOnInit() {
    this.toppingsForOrder = [];
    this.selectedToppings = [];
    this.getAllSizesFromService();
    this.getAllPizzaTypsFromService();
    this.getAllToppings();
    this.createValidation();
  }

  createValidation() {
    this.submitForm = this.formBuilder.group({
      sizeId: this.sizeId,
      pizzaTypeId: this.pizzaTypeId,
      numberOfPizza: this.numberOfPizza,
      edgeTypeId: this.edgeTypeId
    });
  }

  get f() {
    return this.submitForm.controls;
  }

  getAllSizesFromService() {
    this.orderService.getAllSizes().subscribe(res => {
      this.sizesForOrder = res;
    });
  }

  getPizzaId(event) {
    this.selectedPizzaTypeId = event.value;
    var pizzaPrice : any;
    pizzaPrice = this.pizzaTypesForOrder.filter(x => x.id == this.selectedPizzaTypeId);
  
    var pizzaSize = this.sizesForOrder.filter(x => x.id == this.selectedPizzaSizeId);
    if(this.edgeTypeId.value == 1)
    {
    this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) + 50 ;
    }
    else
    {
      this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) ;
    }
  }

  getPizzaSizeId(event) {
    this.selectedPizzaSizeId = event.value;
  }
  
  radioButtonChange(event) {
    this.selectedPizzaCategoryeId = this.pizzaCategoryId.value;
    this.selectedPizzaTypesForOrder = this.pizzaTypesForOrder.filter(x => x.category == "Veg");
    if(this.selectedPizzaCategoryeId == 1)
    {
      this.selectedPizzaTypesForOrder = this.pizzaTypesForOrder.filter(x => x.category == "NonVeg");
    }

    var pizzaPrice : any;
    pizzaPrice = this.pizzaTypesForOrder.filter(x => x.id == this.selectedPizzaTypeId);
  
    var pizzaSize = this.sizesForOrder.filter(x => x.id == this.selectedPizzaSizeId);
    if(this.edgeTypeId.value == 1)
    {
    this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) + 50 ;
    }
    else
    {
      this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) ;
    }
    
  }
  radioButtonChangeCheese(event) {
   
    var pizzaPrice : any;
    pizzaPrice = this.pizzaTypesForOrder.filter(x => x.id == this.selectedPizzaTypeId);
  
    var pizzaSize = this.sizesForOrder.filter(x => x.id == this.selectedPizzaSizeId);
    if(this.edgeTypeId.value == 1)
    {
    this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) + 50 ;
    }
    else
    {
      this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) ;
    }
  
   
  }
  getPizzaNumber(event) {
    this.selectedPizzaNumber = event.data;
   if(this.selectedPizzaNumber > 0)
   {
    var pizzaPrice : any;
    pizzaPrice = this.pizzaTypesForOrder.filter(x => x.id == this.selectedPizzaTypeId);
  
    var pizzaSize = this.sizesForOrder.filter(x => x.id == this.selectedPizzaSizeId);
    if(this.edgeTypeId.value == 1)
    {
    this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) + 50 ;
    }
    else
    {
      this.selectedPizzaPrice = (pizzaPrice[0].price) * (pizzaSize[0].multiplier) ;
    }
    this.totalPrice = this.selectedPizzaNumber * (pizzaPrice[0].price) * (pizzaSize[0].multiplier);
   }
  }
  getAllPizzaTypsFromService() {
    this.orderService.getAllPizzaTypes().subscribe(res => {
      this.pizzaTypesForOrder = res;
      this.selectedPizzaTypesForOrder = this.pizzaTypesForOrder.filter(x => x.category == "Veg");
    });
  }

  getAllToppings() {
    this.orderService.getToppings().subscribe(res => {
      this.toppingsForOrder = res;
    });
  }

  onChange(event) {
    if (event.checked) {
      this.selectedToppings.push(event.source.value);
      if(this.selectedToppings.length > 0)
      {
        this.selectedToppingsNumber = this.selectedToppings.length;
      }
      else
      {
        this.selectedToppingsNumber = 0
      }
    } else {
      const i = this.selectedToppings.findIndex(x => x === event.source.value);
      this.selectedToppings.splice(i, 1);
      if(this.selectedToppings.length > 0)
      {
        this.selectedToppingsNumber = this.selectedToppings.length;
      }
      else
      {
        this.selectedToppingsNumber = 0
      }
    }
  }

  onSubmit() {
    const x = this.selectedToppings;
    this.submitted = true;
    if (this.submitForm.invalid) {
      return;
    }
    this.pizzaToAddCart = this.submitForm.value;
    this.pizzaToAddCart.toppings = this.selectedToppings;
    this.pizzaToAddCart.edgeTypeId = Number(this.edgeTypeId.value);
    this.sendPizzasToService();
  }


  sendPizzasToService() {
    this.orderService.submitPizza(this.pizzaToAddCart).subscribe(res => {
      this.sendTotalNumbersToNavbar(res);
    });
  }

  sendTotalNumbersToNavbar(total: number) {
    this.sharedService.sendTotalNumber(total);
  }
}
