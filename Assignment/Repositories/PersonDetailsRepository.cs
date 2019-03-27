namespace Assignment.Repositories
{
    using Assignment.Models;
    using Assignment.Repositories.Abstracts;
    using System;
    using System.Collections.Generic;

    public class PersonDetailsRepository : BaseEFRepository<PersonDetails, DataAccessDbContext, PersonDetailsRepository>
    {
        public PersonDetailsRepository() : base(new DataAccessDbContext())
        {

        }

        #region Public Methods

        /// <summary>
        /// Create Person Details in PersonDetails Table
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public bool AddPersonDetails(PersonDetails details)
        {
            return base.Create(details);
        }

        /// <summary>
        /// Get all Person Details
        /// </summary>
        /// <returns>IEnumerable<PersonDetailsViewModel></returns>
        public IEnumerable<PersonDetailsViewModel> GetAllPersonDetails()
        {
            var personDetailsList = base.Get();

            var model = new List<PersonDetailsViewModel>();

            foreach (PersonDetails details in personDetailsList)
            {
                var personDetailModel = new PersonDetailsViewModel()
                {
                    Name = details.Name,
                    Number = details.Number,
                    NumberInWords = ConvertNumberToWords(details.Number.ToString())
                };

                model.Add(personDetailModel);
            }
            return model;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Convert Number to Words. This is start function.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ConvertNumberToWords(string number)
        {
            string value = string.Empty, wholeNumber = number, points = string.Empty, pointString = string.Empty;
            try
            {
                int decimalPlace = number.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNumber = number.Substring(0, decimalPlace);
                    points = number.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        //Converting numbers after digit to words and appending required words, like And & Cents.
                        pointString = string.Format("{0} {1} {2}", Constants.And, ConvertWholeNumber(points).Trim(), Constants.Cents);
                    }
                }

                //Generate and concatinate the words after final transalation.
                value = string.Format("{0} {1} {2}", ConvertWholeNumber(wholeNumber).Trim(), Constants.Dollars, pointString);
            }
            catch
            {
                throw;
            }
            return value;
        }

        /// <summary>
        /// Convert whole number which is before digits
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ConvertWholeNumber(string number)
        {
            string word = string.Empty;
            try
            {
                //Variable to validate if number value is already translated
                bool isDone = false;
                double doubleNumber = (Convert.ToDouble(number));

                if (doubleNumber > 0)
                {
                    int numDigits = number.Length;
                    int length = 0;
                    string place = string.Empty;
                    switch (numDigits)
                    {
                        case 1:
                            word = ConvertOnes(number);
                            isDone = true;
                            break;
                        case 2:
                            word = ConvertTens(number);
                            isDone = true;
                            break;
                        case 3:
                            length = (numDigits % 3) + 1;
                            place = string.Format("{0}{1}{2}", Constants.Space, Constants.Hundred, Constants.Space);
                            break;
                        case 4://Range for Thousands    
                        case 5:
                        case 6:
                            length = (numDigits % 4) + 1;
                            place = string.Format("{0}{1}{2}", Constants.Space, Constants.Thousand, Constants.Space);
                            break;
                        case 7:// Range for Millions
                        case 8:
                        case 9:
                            length = (numDigits % 7) + 1;
                            place = string.Format("{0}{1}{2}", Constants.Space, Constants.Million, Constants.Space);
                            break;
                        case 10://Range for Billions    
                        case 11:
                        case 12:
                            length = (numDigits % 10) + 1;
                            place = string.Format("{0}{1}{2}", Constants.Space, Constants.Billion, Constants.Space);
                            break;
                        default:
                            isDone = true;
                            break;
                    }

                    if (!isDone)
                    {
                        //if transalation of number is not done, then continue    
                        if (number.Substring(0, length) != "0" && number.Substring(length) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(number.Substring(0, length)) + place + ConvertWholeNumber(number.Substring(length));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(number.Substring(0, length)) + ConvertWholeNumber(number.Substring(length));
                        }
                    }

                    if (word.Trim().Equals(place.Trim()))
                    {
                        word = string.Empty;
                    }
                }
            }
            catch {
                throw;
            }

            return word.Trim();
        }

        private static string ConvertTens(string number)
        {
            int value = Convert.ToInt32(number);
            string valueName = string.Empty;
            switch (value)
            {
                case 10:
                    valueName = Constants.Ten;
                    break;
                case 11:
                    valueName = Constants.Eleven;
                    break;
                case 12:
                    valueName = Constants.Twelve;
                    break;
                case 13:
                    valueName = Constants.Thirteen;
                    break;
                case 14:
                    valueName = Constants.Fourteen;
                    break;
                case 15:
                    valueName = Constants.Fifteen;
                    break;
                case 16:
                    valueName = Constants.Sixteen;
                    break;
                case 17:
                    valueName = Constants.Seventeen;
                    break;
                case 18:
                    valueName = Constants.Eighteen;
                    break;
                case 19:
                    valueName = Constants.Nineteen;
                    break;
                case 20:
                    valueName = Constants.Twenty;
                    break;
                case 30:
                    valueName = Constants.Thirty;
                    break;
                case 40:
                    valueName = Constants.Fourty;
                    break;
                case 50:
                    valueName = Constants.Fifty;
                    break;
                case 60:
                    valueName = Constants.Sixty;
                    break;
                case 70:
                    valueName = Constants.Seventy;
                    break;
                case 80:
                    valueName = Constants.Eighty;
                    break;
                case 90:
                    valueName = Constants.Ninety;
                    break;
                default:
                    if (value > 0)
                    {
                        valueName = ConvertTens(number.Substring(0, 1) + "0") + Constants.Space + ConvertOnes(number.Substring(1));
                    }
                    break;
            }
            return valueName;
        }

        private static string ConvertOnes(string number)
        {
            int value = Convert.ToInt32(number);
            string valueName = string.Empty;
            switch (value)
            {
                case 1:
                    valueName = Constants.One;
                    break;
                case 2:
                    valueName = Constants.Two;
                    break;
                case 3:
                    valueName = Constants.Three;
                    break;
                case 4:
                    valueName = Constants.Four;
                    break;
                case 5:
                    valueName = Constants.Five;
                    break;
                case 6:
                    valueName = Constants.Six;
                    break;
                case 7:
                    valueName = Constants.Seven;
                    break;
                case 8:
                    valueName = Constants.Eight;
                    break;
                case 9:
                    valueName = Constants.Nine;
                    break;
            }
            return valueName;
        }

        #endregion
    }
}