using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataHelpers
{
    static public class CountriesList
    {
        static public Country[] getCodes() {
            Country[] codes = new Country[243]{
             new Country{
                name= "Afghanistan",
                code= "AF"
            },
            new Country{
                name= "Åland Islands",
                code= "AX"
            },
            new Country{
                name= "Albania",
                code= "AL"
            },
            new Country{
                name= "Algeria",
                code= "DZ"
            },
            new Country{
                name= "American Samoa",
                code= "AS"
            },
            new Country{
                name= "AndorrA",
                code= "AD"
            },
            new Country{
                name= "Angola",
                code= "AO"
            },
            new Country{
                name= "Anguilla",
                code= "AI"
            },
            new Country{
                name= "Antarctica",
                code= "AQ"
            },
            new Country{
                name= "Antigua and Barbuda",
                code= "AG"
            },
            new Country{
                name= "Argentina",
                code= "AR"
            },
            new Country{
                name= "Armenia",
                code= "AM"
            },
            new Country{
                name= "Aruba",
                code= "AW"
            },
            new Country{
                name= "Australia",
                code= "AU"
            },
            new Country{
                name= "Austria",
                code= "AT"
            },
            new Country{
                name= "Azerbaijan",
                code= "AZ"
            },
            new Country{
                name= "Bahamas",
                code= "BS"
            },
            new Country{
                name= "Bahrain",
                code= "BH"
            },
            new Country{
                name= "Bangladesh",
                code= "BD"
            },
            new Country{
                name= "Barbados",
                code= "BB"
            },
            new Country{
                name= "Belarus",
                code= "BY"
            },
            new Country{
                name= "Belgium",
                code= "BE"
            },
            new Country{
                name= "Belize",
                code= "BZ"
            },
            new Country{
                name= "Benin",
                code= "BJ"
            },
            new Country{
                name= "Bermuda",
                code= "BM"
            },
            new Country{
                name= "Bhutan",
                code= "BT"
            },
            new Country{
                name= "Bolivia",
                code= "BO"
            },
            new Country{
                name= "Bosnia and Herzegovina",
                code= "BA"
            },
            new Country{
                name= "Botswana",
                code= "BW"
            },
            new Country{
                name= "Bouvet Island",
                code= "BV"
            },
            new Country{
                name= "Brazil",
                code= "BR"
            },
            new Country{
                name= "British Indian Ocean Territory",
                code= "IO"
            },
            new Country{
                name= "Brunei Darussalam",
                code= "BN"
            },
            new Country{
                name= "Bulgaria",
                code= "BG"
            },
            new Country{
                name= "Burkina Faso",
                code= "BF"
            },
            new Country{
                name= "Burundi",
                code= "BI"
            },
            new Country{
                name= "Cambodia",
                code= "KH"
            },
            new Country{
                name= "Cameroon",
                code= "CM"
            },
            new Country{
                name= "Canada",
                code= "CA"
            },
            new Country{
                name= "Cape Verde",
                code= "CV"
            },
            new Country{
                name= "Cayman Islands",
                code= "KY"
            },
            new Country{
                name= "Central African Republic",
                code= "CF"
            },
            new Country{
                name= "Chad",
                code= "TD"
            },
            new Country{
                name= "Chile",
                code= "CL"
            },
            new Country{
                name= "China",
                code= "CN"
            },
            new Country{
                name= "Christmas Island",
                code= "CX"
            },
            new Country{
                name= "Cocos (Keeling) Islands",
                code= "CC"
            },
            new Country{
                name= "Colombia",
                code= "CO"
            },
            new Country{
                name= "Comoros",
                code= "KM"
            },
            new Country{
                name= "Congo",
                code= "CG"
            },
            new Country{
                name= "Congo, The Democratic Republic of the",
                code= "CD"
            },
            new Country{
                name= "Cook Islands",
                code= "CK"
            },
            new Country{
                name= "Costa Rica",
                code= "CR"
            },
            new Country{
                name= "Cote D\"Ivoire",
                code= "CI"
            },
            new Country{
                name= "Croatia",
                code= "HR"
            },
            new Country{
                name= "Cuba",
                code= "CU"
            },
            new Country{
                name= "Cyprus",
                code= "CY"
            },
            new Country{
                name= "Czech Republic",
                code= "CZ"
            },
            new Country{
                name= "Denmark",
                code= "DK"
            },
            new Country{
                name= "Djibouti",
                code= "DJ"
            },
            new Country{
                name= "Dominica",
                code= "DM"
            },
            new Country{
                name= "Dominican Republic",
                code= "DO"
            },
            new Country{
                name= "Ecuador",
                code= "EC"
            },
            new Country{
                name= "Egypt",
                code= "EG"
            },
            new Country{
                name= "El Salvador",
                code= "SV"
            },
            new Country{
                name= "Equatorial Guinea",
                code= "GQ"
            },
            new Country{
                name= "Eritrea",
                code= "ER"
            },
            new Country{
                name= "Estonia",
                code= "EE"
            },
            new Country{
                name= "Ethiopia",
                code= "ET"
            },
            new Country{
                name= "Falkland Islands (Malvinas)",
                code= "FK"
            },
            new Country{
                name= "Faroe Islands",
                code= "FO"
            },
            new Country{
                name= "Fiji",
                code= "FJ"
            },
            new Country{
                name= "Finland",
                code= "FI"
            },
            new Country{
                name= "France",
                code= "FR"
            },
            new Country{
                name= "French Guiana",
                code= "GF"
            },
            new Country{
                name= "French Polynesia",
                code= "PF"
            },
            new Country{
                name= "French Southern Territories",
                code= "TF"
            },
            new Country{
                name= "Gabon",
                code= "GA"
            },
            new Country{
                name= "Gambia",
                code= "GM"
            },
            new Country{
                name= "Georgia",
                code= "GE"
            },
            new Country{
                name= "Germany",
                code= "DE"
            },
            new Country{
                name= "Ghana",
                code= "GH"
            },
            new Country{
                name= "Gibraltar",
                code= "GI"
            },
            new Country{
                name= "Greece",
                code= "GR"
            },
            new Country{
                name= "Greenland",
                code= "GL"
            },
            new Country{
                name= "Grenada",
                code= "GD"
            },
            new Country{
                name= "Guadeloupe",
                code= "GP"
            },
            new Country{
                name= "Guam",
                code= "GU"
            },
            new Country{
                name= "Guatemala",
                code= "GT"
            },
            new Country{
                name= "Guernsey",
                code= "GG"
            },
            new Country{
                name= "Guinea",
                code= "GN"
            },
            new Country{
                name= "Guinea-Bissau",
                code= "GW"
            },
            new Country{
                name= "Guyana",
                code= "GY"
            },
            new Country{
                name= "Haiti",
                code= "HT"
            },
            new Country{
                name= "Heard Island and Mcdonald Islands",
                code= "HM"
            },
            new Country{
                name= "Holy See (Vatican City State)",
                code= "VA"
            },
            new Country{
                name= "Honduras",
                code= "HN"
            },
            new Country{
                name= "Hong Kong",
                code= "HK"
            },
            new Country{
                name= "Hungary",
                code= "HU"
            },
            new Country{
                name= "Iceland",
                code= "IS"
            },
            new Country{
                name= "India",
                code= "IN"
            },
            new Country{
                name= "Indonesia",
                code= "ID"
            },
            new Country{
                name= "Iran, Islamic Republic Of",
                code= "IR"
            },
            new Country{
                name= "Iraq",
                code= "IQ"
            },
            new Country{
                name= "Ireland",
                code= "IE"
            },
            new Country{
                name= "Isle of Man",
                code= "IM"
            },
            new Country{
                name= "Israel",
                code= "IL"
            },
            new Country{
                name= "Italy",
                code= "IT"
            },
            new Country{
                name= "Jamaica",
                code= "JM"
            },
            new Country{
                name= "Japan",
                code= "JP"
            },
            new Country{
                name= "Jersey",
                code= "JE"
            },
            new Country{
                name= "Jordan",
                code= "JO"
            },
            new Country{
                name= "Kazakhstan",
                code= "KZ"
            },
            new Country{
                name= "Kenya",
                code= "KE"
            },
            new Country{
                name= "Kiribati",
                code= "KI"
            },
            new Country{
                name= "Korea, Democratic People\"S Republic of",
                code= "KP"
            },
            new Country{
                name= "Korea, Republic of",
                code= "KR"
            },
            new Country{
                name= "Kuwait",
                code= "KW"
            },
            new Country{
                name= "Kyrgyzstan",
                code= "KG"
            },
            new Country{
                name= "Lao People\"S Democratic Republic",
                code= "LA"
            },
            new Country{
                name= "Latvia",
                code= "LV"
            },
            new Country{
                name= "Lebanon",
                code= "LB"
            },
            new Country{
                name= "Lesotho",
                code= "LS"
            },
            new Country{
                name= "Liberia",
                code= "LR"
            },
            new Country{
                name= "Libyan Arab Jamahiriya",
                code= "LY"
            },
            new Country{
                name= "Liechtenstein",
                code= "LI"
            },
            new Country{
                name= "Lithuania",
                code= "LT"
            },
            new Country{
                name= "Luxembourg",
                code= "LU"
            },
            new Country{
                name= "Macao",
                code= "MO"
            },
            new Country{
                name= "Macedonia, The Former Yugoslav Republic of",
                code= "MK"
            },
            new Country{
                name= "Madagascar",
                code= "MG"
            },
            new Country{
                name= "Malawi",
                code= "MW"
            },
            new Country{
                name= "Malaysia",
                code= "MY"
            },
            new Country{
                name= "Maldives",
                code= "MV"
            },
            new Country{
                name= "Mali",
                code= "ML"
            },
            new Country{
                name= "Malta",
                code= "MT"
            },
            new Country{
                name= "Marshall Islands",
                code= "MH"
            },
            new Country{
                name= "Martinique",
                code= "MQ"
            },
            new Country{
                name= "Mauritania",
                code= "MR"
            },
            new Country{
                name= "Mauritius",
                code= "MU"
            },
            new Country{
                name= "Mayotte",
                code= "YT"
            },
            new Country{
                name= "Mexico",
                code= "MX"
            },
            new Country{
                name= "Micronesia, Federated States of",
                code= "FM"
            },
            new Country{
                name= "Moldova, Republic of",
                code= "MD"
            },
            new Country{
                name= "Monaco",
                code= "MC"
            },
            new Country{
                name= "Mongolia",
                code= "MN"
            },
            new Country{
                name= "Montserrat",
                code= "MS"
            },
            new Country{
                name= "Morocco",
                code= "MA"
            },
            new Country{
                name= "Mozambique",
                code= "MZ"
            },
            new Country{
                name= "Myanmar",
                code= "MM"
            },
            new Country{
                name= "Namibia",
                code= "NA"
            },
            new Country{
                name= "Nauru",
                code= "NR"
            },
            new Country{
                name= "Nepal",
                code= "NP"
            },
            new Country{
                name= "Netherlands",
                code= "NL"
            },
            new Country{
                name= "Netherlands Antilles",
                code= "AN"
            },
            new Country{
                name= "New Caledonia",
                code= "NC"
            },
            new Country{
                name= "New Zealand",
                code= "NZ"
            },
            new Country{
                name= "Nicaragua",
                code= "NI"
            },
            new Country{
                name= "Niger",
                code= "NE"
            },
            new Country{
                name= "Nigeria",
                code= "NG"
            },
            new Country{
                name= "Niue",
                code= "NU"
            },
            new Country{
                name= "Norfolk Island",
                code= "NF"
            },
            new Country{
                name= "Northern Mariana Islands",
                code= "MP"
            },
            new Country{
                name= "Norway",
                code= "NO"
            },
            new Country{
                name= "Oman",
                code= "OM"
            },
            new Country{
                name= "Pakistan",
                code= "PK"
            },
            new Country{
                name= "Palau",
                code= "PW"
            },
            new Country{
                name= "Palestinian Territory, Occupied",
                code= "PS"
            },
            new Country{
                name= "Panama",
                code= "PA"
            },
            new Country{
                name= "Papua New Guinea",
                code= "PG"
            },
            new Country{
                name= "Paraguay",
                code= "PY"
            },
            new Country{
                name= "Peru",
                code= "PE"
            },
            new Country{
                name= "Philippines",
                code= "PH"
            },
            new Country{
                name= "Pitcairn",
                code= "PN"
            },
            new Country{
                name= "Poland",
                code= "PL"
            },
            new Country{
                name= "Portugal",
                code= "PT"
            },
            new Country{
                name= "Puerto Rico",
                code= "PR"
            },
            new Country{
                name= "Qatar",
                code= "QA"
            },
            new Country{
                name= "Reunion",
                code= "RE"
            },
            new Country{
                name= "Romania",
                code= "RO"
            },
            new Country{
                name= "Russian Federation",
                code= "RU"
            },
            new Country{
                name= "RWANDA",
                code= "RW"
            },
            new Country{
                name= "Saint Helena",
                code= "SH"
            },
            new Country{
                name= "Saint Kitts and Nevis",
                code= "KN"
            },
            new Country{
                name= "Saint Lucia",
                code= "LC"
            },
            new Country{
                name= "Saint Pierre and Miquelon",
                code= "PM"
            },
            new Country{
                name= "Saint Vincent and the Grenadines",
                code= "VC"
            },
            new Country{
                name= "Samoa",
                code= "WS"
            },
            new Country{
                name= "San Marino",
                code= "SM"
            },
            new Country{
                name= "Sao Tome and Principe",
                code= "ST"
            },
            new Country{
                name= "Saudi Arabia",
                code= "SA"
            },
            new Country{
                name= "Senegal",
                code= "SN"
            },
            new Country{
                name= "Serbia and Montenegro",
                code= "CS"
            },
            new Country{
                name= "Seychelles",
                code= "SC"
            },
            new Country{
                name= "Sierra Leone",
                code= "SL"
            },
            new Country{
                name= "Singapore",
                code= "SG"
            },
            new Country{
                name= "Slovakia",
                code= "SK"
            },
            new Country{
                name= "Slovenia",
                code= "SI"
            },
            new Country{
                name= "Solomon Islands",
                code= "SB"
            },
            new Country{
                name= "Somalia",
                code= "SO"
            },
            new Country{
                name= "South Africa",
                code= "ZA"
            },
            new Country{
                name= "South Georgia and the South Sandwich Islands",
                code= "GS"
            },
            new Country{
                name= "Spain",
                code= "ES"
            },
            new Country{
                name= "Sri Lanka",
                code= "LK"
            },
            new Country{
                name= "Sudan",
                code= "SD"
            },
            new Country{
                name= "Suriname",
                code= "SR"
            },
            new Country{
                name= "Svalbard and Jan Mayen",
                code= "SJ"
            },
            new Country{
                name= "Swaziland",
                code= "SZ"
            },
            new Country{
                name= "Sweden",
                code= "SE"
            },
            new Country{
                name= "Switzerland",
                code= "CH"
            },
            new Country{
                name= "Syrian Arab Republic",
                code= "SY"
            },
            new Country{
                name= "Taiwan, Province of China",
                code= "TW"
            },
            new Country{
                name= "Tajikistan",
                code= "TJ"
            },
            new Country{
                name= "Tanzania, United Republic of",
                code= "TZ"
            },
            new Country{
                name= "Thailand",
                code= "TH"
            },
            new Country{
                name= "Timor-Leste",
                code= "TL"
            },
            new Country{
                name= "Togo",
                code= "TG"
            },
            new Country{
                name= "Tokelau",
                code= "TK"
            },
            new Country{
                name= "Tonga",
                code= "TO"
            },
            new Country{
                name= "Trinidad and Tobago",
                code= "TT"
            },
            new Country{
                name= "Tunisia",
                code= "TN"
            },
            new Country{
                name= "Turkey",
                code= "TR"
            },
            new Country{
                name= "Turkmenistan",
                code= "TM"
            },
            new Country{
                name= "Turks and Caicos Islands",
                code= "TC"
            },
            new Country{
                name= "Tuvalu",
                code= "TV"
            },
            new Country{
                name= "Uganda",
                code= "UG"
            },
            new Country{
                name= "Ukraine",
                code= "UA"
            },
            new Country{
                name= "United Arab Emirates",
                code= "AE"
            },
            new Country{
                name= "United Kingdom",
                code= "GB"
            },
            new Country{
                name= "United States",
                code= "US"
            },
            new Country{
                name= "United States Minor Outlying Islands",
                code= "UM"
            },
            new Country{
                name= "Uruguay",
                code= "UY"
            },
            new Country{
                name= "Uzbekistan",
                code= "UZ"
            },
            new Country{
                name= "Vanuatu",
                code= "VU"
            },
            new Country{
                name= "Venezuela",
                code= "VE"
            },
            new Country{
                name= "Viet Nam",
                code= "VN"
            },
            new Country{
                name= "Virgin Islands, British",
                code= "VG"
            },
            new Country{
                name= "Virgin Islands, U.S.",
                code= "VI"
            },
            new Country{
                name= "Wallis and Futuna",
                code= "WF"
            },
            new Country{
                name= "Western Sahara",
                code= "EH"
            },
            new Country{
                name= "Yemen",
                code= "YE"
            },
            new Country{
                name= "Zambia",
                code= "ZM"
            },
            new Country{
                name= "Zimbabwe",
                code= "ZW"
            }          
        };
            return codes;
        }
        static public bool checkCode(string code)
        {
            Country[] codes = getCodes();
            for (int i = 0; i < codes.Length; i++)
            {
                if (code.Equals(codes[i].code))
                    return true;
            }
            return false;
        }
        static public Country getCountry(string code)
        {
            Country[] codes = getCodes();
            for (int i = 0; i < codes.Length; i++)
            {
                if (code.Equals(codes[i].code))
                    return codes[i];
            }
            return null;
        }
    }
}             
     
 
