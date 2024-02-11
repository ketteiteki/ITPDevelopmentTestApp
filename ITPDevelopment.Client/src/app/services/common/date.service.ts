import {Injectable} from '@angular/core';
import dayjs from "dayjs";
import {differenceInMinutes} from "date-fns";

@Injectable({
  providedIn: 'root'
})
export class DateService {

  constructor() { }

  public getTime(dateString: string) {
    return dayjs(dateString).format("HH:mm");
  }

  public getSubtract(firstDateString: string, secondDateString: string) {
    const date1 = new Date(firstDateString);
    const date2 = new Date(secondDateString);

    const differenceInMinutesValue = Math.abs(differenceInMinutes(date1, date2));

    const hours = Math.floor(differenceInMinutesValue / 60);
    const minutes = differenceInMinutesValue % 60;

    return `${hours < 10 ? '0' : ''}${hours}:${minutes < 10 ? '0' : ''}${minutes}`;
  }
}
