using System.ComponentModel;
using Template.Common.Attributes;

namespace Template.Entities.Enums
{
    public enum Country
    {
        [Country2ISOCode("AF")]
        Afghanistan = 004,

        [Description("Åland Islands")]
        [Country2ISOCode("AX")]
        AlandIslands = 248,

        [Country2ISOCode("AL")]
        Albania = 008,

        [Country2ISOCode("DZ")]
        Algeria = 012,

        [Description("American Samoa")]
        [Country2ISOCode("AS")]
        AmericanSamoa = 016,

        [Country2ISOCode("AD")]
        Andorra = 020,

        [Country2ISOCode("AO")]
        Angola = 024,

        [Country2ISOCode("AI")]
        Anguilla = 660,

        [Country2ISOCode("AQ")]
        Antarctica = 010,

        [Description("Antigua and Barbuda")]
        [Country2ISOCode("AG")]
        AntiguaAndBarbuda = 028,

        [Country2ISOCode("AR")]
        Argentina = 032,

        [Country2ISOCode("AM")]
        Armenia = 051,

        [Country2ISOCode("AW")]
        Aruba = 533,

        [Country2ISOCode("AU")]
        Australia = 036,

        [Country2ISOCode("AT")]
        Austria = 040,

        [Country2ISOCode("AZ")]
        Azerbaijan = 031,

        [Country2ISOCode("BS")]
        Bahamas = 044,

        [Country2ISOCode("BH")]
        Bahrain = 048,

        [Country2ISOCode("BD")]
        Bangladesh = 050,

        [Country2ISOCode("BB")]
        Barbados = 052,

        [Country2ISOCode("BY")]
        Belarus = 112,

        [Country2ISOCode("BE")]
        Belgium = 056,

        [Country2ISOCode("BZ")]
        Belize = 084,

        [Country2ISOCode("BJ")]
        Benin = 204,

        [Country2ISOCode("BM")]
        Bermuda = 060,

        [Country2ISOCode("BT")]
        Bhutan = 064,

        [Description("Bolivia (Plurinational State of)")]
        [Country2ISOCode("BO")]
        Bolivia = 068,

        [Description("Bosnia and Herzegovina")]
        [Country2ISOCode("BA")]
        BosniaAndHerzegovina = 070,

        [Country2ISOCode("BW")]
        Botswana = 072,

        [Description("Bouvet Island")]
        [Country2ISOCode("BV")]
        BouvetIsland = 074,

        [Country2ISOCode("BR")]
        Brazil = 076,

        [Description("British Indian Ocean Territory")]
        [Country2ISOCode("IO")]
        BritishIndianOceanTerritory = 086,

        [Description("Brunei Darussalam")]
        [Country2ISOCode("BN")]
        BruneiDarussalam = 096,

        [Country2ISOCode("BG")]
        Bulgaria = 100,

        [Description("Burkina Faso")]
        [Country2ISOCode("BF")]
        BurkinaFaso = 854,

        [Country2ISOCode("BI")]
        Burundi = 108,

        [Description("Cabo Verde")]
        [Country2ISOCode("CV")]
        CaboVerde = 132,

        [Country2ISOCode("KH")]
        Cambodia = 116,

        [Country2ISOCode("CM")]
        Cameroon = 120,

        [Country2ISOCode("CA")]
        Canada = 124,

        [Description("Cayman Islands")]
        [Country2ISOCode("KY")]
        CaymanIslands = 136,

        [Description("Central African Republic")]
        [Country2ISOCode("CF")]
        CentralAfricanRepublic = 140,

        [Country2ISOCode("TD")]
        Chad = 148,

        [Country2ISOCode("CL")]
        Chile = 152,

        [Country2ISOCode("CN")]
        China = 156,

        [Description("Christmas Island")]
        [Country2ISOCode("CX")]
        ChristmasIsland = 162,

        [Description("Cocos (Keeling) Islands")]
        [Country2ISOCode("CC")]
        CocosIslands = 166,

        [Country2ISOCode("CO")]
        Colombia = 170,

        [Country2ISOCode("KM")]
        Comoros = 174,

        [Description("Congo")]
        [Country2ISOCode("CG")]
        RepublicoftheCongo = 178,

        [Description("Congo (Democratic Republic of the)")]
        [Country2ISOCode("CD")]
        Congo = 180,

        [Description("Cook Islands")]
        [Country2ISOCode("CK")]
        CookIslands = 184,

        [Description("Costa Rica")]
        [Country2ISOCode("CR")]
        CostaRica = 188,

        [Description("Côte d'Ivoire")]
        [Country2ISOCode("CI")]
        CotedIvoire = 384,

        [Country2ISOCode("HR")]
        Croatia = 191,

        [Country2ISOCode("CU")]
        Cuba = 192,

        [Description("Curaçao")]
        Curacao = 531,

        [Country2ISOCode("CY")]
        Cyprus = 196,

        [Country2ISOCode("CZ")]
        Czechia = 203,

        [Country2ISOCode("DK")]
        Denmark = 208,

        [Country2ISOCode("DJ")]
        Djibouti = 262,

        [Country2ISOCode("DM")]
        Dominica = 212,

        [Description("Dominican Republic")]
        [Country2ISOCode("DO")]
        DominicanRepublic = 214,

        [Country2ISOCode("EC")]
        Ecuador = 218,

        [Country2ISOCode("EG")]
        Egypt = 818,

        [Description("El Salvador")]
        [Country2ISOCode("SV")]
        ElSalvador = 222,

        [Description("Equatorial Guinea")]
        [Country2ISOCode("GQ")]
        EquatorialGuinea = 226,

        [Country2ISOCode("ER")]
        Eritrea = 232,

        [Country2ISOCode("EE")]
        Estonia = 233,

        [Country2ISOCode("ET")]
        Ethiopia = 231,

        [Description("Falkland Islands (Malvinas)")]
        [Country2ISOCode("FK")]
        FalklandIslands = 238,

        [Description("Faroe Islands")]
        [Country2ISOCode("FO")]
        FaroeIslands = 234,

        [Country2ISOCode("FJ")]
        Fiji = 242,

        [Country2ISOCode("FI")]
        Finland = 246,

        [Country2ISOCode("FR")]
        France = 250,

        [Description("French Guiana")]
        [Country2ISOCode("GF")]
        FrenchGuiana = 254,

        [Description("French Polynesia")]
        [Country2ISOCode("PF")]
        FrenchPolynesia = 258,

        [Description("French Southern Territories")]
        [Country2ISOCode("TF")]
        FrenchSouthernTerritories = 260,

        [Country2ISOCode("GA")]
        Gabon = 266,

        [Country2ISOCode("GM")]
        Gambia = 270,

        [Country2ISOCode("GE")]
        Georgia = 268,

        [Country2ISOCode("DE")]
        Germany = 276,

        [Country2ISOCode("GH")]
        Ghana = 288,

        [Country2ISOCode("GI")]
        Gibraltar = 292,

        [Country2ISOCode("GR")]
        Greece = 300,

        [Country2ISOCode("GL")]
        Greenland = 304,

        [Country2ISOCode("GD")]
        Grenada = 308,

        [Country2ISOCode("GP")]
        Guadeloupe = 312,

        [Country2ISOCode("GU")]
        Guam = 316,

        [Country2ISOCode("GT")]
        Guatemala = 320,

        [Country2ISOCode("GG")]
        Guernsey = 831,

        [Country2ISOCode("GN")]
        Guinea = 324,

        [Description("Guinea-Bissau")]
        [Country2ISOCode("GW")]
        GuineaBissau = 624,

        [Country2ISOCode("GY")]
        Guyana = 328,

        [Country2ISOCode("HT")]
        Haiti = 332,

        [Description("Heard Island and McDonald Islands")]
        [Country2ISOCode("HM")]
        HeardIslandandMcDonaldIslands = 334,

        [Description("Holy See")]
        [Country2ISOCode("VA")]
        VaticanCityState = 336,

        [Country2ISOCode("HN")]
        Honduras = 340,

        [Description("Hong Kong")]
        [Country2ISOCode("HK")]
        HongKong = 344,

        [Country2ISOCode("HU")]
        Hungary = 348,

        [Country2ISOCode("IS")]
        Iceland = 352,

        [Country2ISOCode("IN")]
        India = 356,

        [Country2ISOCode("ID")]
        Indonesia = 360,

        [Description("Iran (Islamic Republic of)")]
        [Country2ISOCode("IR")]
        Iran = 364,

        [Country2ISOCode("IQ")]
        Iraq = 368,

        [Country2ISOCode("IR")]
        RepublicofIreland = 372,

        [Description("Isle of Man")]
        [Country2ISOCode("IM")]
        IsleofMan = 833,

        [Country2ISOCode("I:")]
        Israel = 376,

        [Country2ISOCode("IT")]
        Italy = 380,

        [Country2ISOCode("JM")]
        Jamaica = 388,

        [Country2ISOCode("JP")]
        Japan = 392,

        [Country2ISOCode("JE")]
        Jersey = 832,

        [Country2ISOCode("JO")]
        Jordan = 400,

        [Country2ISOCode("KZ")]
        Kazakhstan = 398,

        [Country2ISOCode("KE")]
        Kenya = 404,

        [Country2ISOCode("KI")]
        Kiribati = 296,

        [Description("Korea (Democratic People's Republic of)")]
        [Country2ISOCode("KP")]
        NorthKorea = 408,

        [Description("Korea (Republic of)")]
        [Country2ISOCode("KR")]
        Korea = 410,

        [Country2ISOCode("KW")]
        Kuwait = 414,

        [Country2ISOCode("KG")]
        Kyrgyzstan = 417,

        [Description("Lao People's Democratic Republic")]
        [Country2ISOCode("LA")]
        LaoPeoplesDemocraticRepublic = 418,

        [Country2ISOCode("LV")]
        Latvia = 428,

        [Country2ISOCode("LB")]
        Lebanon = 422,

        [Country2ISOCode("LS")]
        Lesotho = 426,

        [Country2ISOCode("LR")]
        Liberia = 430,

        [Country2ISOCode("LY")]
        Libya = 434,

        [Country2ISOCode("LI")]
        Liechtenstein = 438,

        [Country2ISOCode("LT")]
        Lithuania = 440,

        [Country2ISOCode("LU")]
        Luxembourg = 442,

        [Country2ISOCode("MO")]
        Macao = 446,

        [Description("Macedonia")]
        [Country2ISOCode("MK")]
        RepublicofMacedonia = 807,

        [Country2ISOCode("MG")]
        Madagascar = 450,

        [Country2ISOCode("MW")]
        Malawi = 454,

        [Country2ISOCode("MY")]
        Malaysia = 458,

        [Country2ISOCode("MV")]
        Maldives = 462,

        [Country2ISOCode("ML")]
        Mali = 466,

        [Country2ISOCode("MT")]
        Malta = 470,

        [Description("Marshall Islands")]
        [Country2ISOCode("MH")]
        MarshallIslands = 584,

        [Country2ISOCode("MQ")]
        Martinique = 474,

        [Country2ISOCode("MR")]
        Mauritania = 478,

        [Country2ISOCode("MU")]
        Mauritius = 480,

        [Country2ISOCode("YT")]
        Mayotte = 175,

        [Country2ISOCode("MX")]
        Mexico = 484,

        [Description("Micronesia (Federated States of)")]
        [Country2ISOCode("FM")]
        Micronesia = 583,

        [Description("Moldova (Republic of)")]
        [Country2ISOCode("MD")]
        Moldova = 498,

        [Country2ISOCode("MC")]
        Monaco = 492,

        [Country2ISOCode("MN")]
        Mongolia = 496,

        [Country2ISOCode("ME")]
        Montenegro = 499,

        [Country2ISOCode("MS")]
        Montserrat = 500,

        [Country2ISOCode("MA")]
        Morocco = 504,

        [Country2ISOCode("MZ")]
        Mozambique = 508,

        [Country2ISOCode("MM")]
        Myanmar = 104,

        [Country2ISOCode("NA")]
        Namibia = 516,

        [Country2ISOCode("NR")]
        Nauru = 520,

        [Country2ISOCode("NP")]
        Nepal = 524,

        [Country2ISOCode("NL")]
        Netherlands = 528,

        [Description("New Caledonia")]
        [Country2ISOCode("NC")]
        NewCaledonia = 540,

        [Description("New Zealand")]
        [Country2ISOCode("NZ")]
        NewZealand = 554,

        [Country2ISOCode("NI")]
        Nicaragua = 558,

        [Country2ISOCode("NE")]
        Niger = 562,

        [Country2ISOCode("NG")]
        Nigeria = 566,

        [Country2ISOCode("NU")]
        Niue = 570,

        [Description("Norfolk Island")]
        [Country2ISOCode("NF")]
        NorfolkIsland = 574,

        [Description("Northern Mariana Islands")]
        [Country2ISOCode("MP")]
        NorthernMarianaIslands = 580,

        [Country2ISOCode("NO")]
        Norway = 578,

        [Country2ISOCode("OM")]
        Oman = 512,

        [Country2ISOCode("PK")]
        Pakistan = 586,

        [Country2ISOCode("PW")]
        Palau = 585,

        [Description("Palestine, State of")]
        [Country2ISOCode("PS")]
        StateofPalestine = 275,

        [Country2ISOCode("PA")]
        Panama = 591,

        [Description("Papua New Guinea")]
        [Country2ISOCode("PG")]
        PapuaNewGuinea = 598,

        [Country2ISOCode("PY")]
        Paraguay = 600,

        [Country2ISOCode("PE")]
        Peru = 604,

        [Country2ISOCode("PH")]
        Philippines = 608,

        [Country2ISOCode("PN")]
        Pitcairn = 612,

        [Country2ISOCode("PL")]
        Poland = 616,

        [Country2ISOCode("PT")]
        Portugal = 620,

        [Description("Puerto Rico")]
        [Country2ISOCode("PR")]
        PuertoRico = 630,

        [Country2ISOCode("QA")]
        Qatar = 634,

        [Description("Réunion")]
        [Country2ISOCode("RE")]
        Reunion = 638,

        [Country2ISOCode("RO")]
        Romania = 642,

        [Description("Russia")]
        [Country2ISOCode("RU")]
        Russia = 643,

        [Country2ISOCode("RW")]
        Rwanda = 646,

        [Description("Saint Barthélemy")]
        [Country2ISOCode("BL")]
        SaintBarthelemy = 652,

        [Description("Saint Helena, Ascension and Tristan da Cunha")]
        [Country2ISOCode("SH")]
        SaintHelenaAscensionandTristandaCunha = 654,

        [Description("Saint Kitts and Nevis")]
        [Country2ISOCode("KN")]
        SaintKittsandNevis = 659,

        [Description("Saint Lucia")]
        [Country2ISOCode("LC")]
        SaintLucia = 662,

        [Description("Saint Martin (French part)")]
        [Country2ISOCode("MF")]
        SaintMartin = 663,

        [Description("Saint Pierre and Miquelon")]
        [Country2ISOCode("PM")]
        SaintPierreandMiquelon = 666,

        [Description("Saint Vincent and the Grenadines")]
        [Country2ISOCode("VC")]
        SaintVincentandtheGrenadines = 670,

        [Country2ISOCode("WC")]
        Samoa = 882,

        [Description("San Marino")]
        [Country2ISOCode("SM")]
        SanMarino = 674,

        [Description("Sao Tome and Principe")]
        [Country2ISOCode("ST")]
        SaoTomeandPrincipe = 678,

        [Description("Saudi Arabia")]
        [Country2ISOCode("SA")]
        SaudiArabia = 682,

        [Country2ISOCode("SN")]
        Senegal = 686,

        [Country2ISOCode("RS")]
        Serbia = 688,

        [Country2ISOCode("SC")]
        Seychelles = 690,

        [Description("Sierra Leone")]
        [Country2ISOCode("SL")]
        SierraLeone = 694,

        [Country2ISOCode("SG")]
        Singapore = 702,

        [Description("Sint Maarten (Dutch part)")]
        SintMaarten = 534,

        [Country2ISOCode("SK")]
        Slovakia = 703,

        [Country2ISOCode("SI")]
        Slovenia = 705,

        [Description("Solomon Islands")]
        [Country2ISOCode("SB")]
        SolomonIslands = 090,

        [Country2ISOCode("SO")]
        Somalia = 706,

        [Description("South Africa")]
        [Country2ISOCode("ZA")]
        SouthAfrica = 710,

        [Description("South Georgia and the South Sandwich Islands")]
        [Country2ISOCode("GS")]
        SouthGeorgiaandtheSouthSandwichIslands = 239,

        [Description("South Sudan")]
        [Country2ISOCode("SS")]
        SouthSudan = 728,

        [Country2ISOCode("ES")]
        Spain = 724,

        [Description("Sri Lanka")]
        [Country2ISOCode("LK")]
        SriLanka = 144,

        [Country2ISOCode("SD")]
        Sudan = 729,

        [Country2ISOCode("SR")]
        Suriname = 740,

        [Description("Svalbard and Jan Mayen")]
        [Country2ISOCode("SJ")]
        SvalbardandJanMayen = 744,

        [Country2ISOCode("SZ")]
        Swaziland = 748,

        [Country2ISOCode("SE")]
        Sweden = 752,

        [Country2ISOCode("CH")]
        Switzerland = 756,

        [Description("Syrian Arab Republic")]
        [Country2ISOCode("SY")]
        SyrianArabRepublic = 760,

        [Description("Taiwan, Province of China")]
        [Country2ISOCode("TW")]
        TaiwanProvinceofChina = 158,

        [Country2ISOCode("TJ")]
        Tajikistan = 762,

        [Description("Tanzania, United Republic of")]
        [Country2ISOCode("TZ")]
        TanzaniaUnitedRepublicof = 834,

        [Country2ISOCode("TH")]
        Thailand = 764,

        [Description("Timor-Leste")]
        [Country2ISOCode("TL")]
        TimorLeste = 626,

        [Country2ISOCode("TG")]
        Togo = 768,

        [Country2ISOCode("TK")]
        Tokelau = 772,

        [Country2ISOCode("TO")]
        Tonga = 776,

        [Description("Trinidad and Tobago")]
        [Country2ISOCode("TT")]
        TrinidadandTobago = 780,

        [Country2ISOCode("TN")]
        Tunisia = 788,

        [Country2ISOCode("TR")]
        Turkey = 792,

        [Country2ISOCode("TM")]
        Turkmenistan = 795,

        [Description("Turks and Caicos Islands")]
        [Country2ISOCode("TC")]
        TurksandCaicosIslands = 796,

        [Country2ISOCode("TV")]
        Tuvalu = 798,

        [Country2ISOCode("UG")]
        Uganda = 800,

        [Country2ISOCode("UA")]
        Ukraine = 804,

        [Description("United Arab Emirates")]
        [Country2ISOCode("AE")]
        UnitedArabEmirates = 784,

        [Description("United Kingdom of Great Britain and Northern Ireland")]
        [Country2ISOCode("GB")]
        UnitedKingdomofGreatBritainandNorthernIreland = 826,

        [Description("United States of America")]
        [Country2ISOCode("US")]
        UnitedStatesofAmerica = 840,

        [Description("United States Minor Outlying Islands")]
        [Country2ISOCode("UM")]
        UnitedStatesMinorOutlyingIslands = 581,

        [Country2ISOCode("UY")]
        Uruguay = 858,

        [Country2ISOCode("UZ")]
        Uzbekistan = 860,

        [Country2ISOCode("VU")]
        Vanuatu = 548,

        [Description("Venezuela (Bolivarian Republic of)")]
        [Country2ISOCode("VE")]
        Venezuela = 862,

        [Description("Viet Nam")]
        [Country2ISOCode("VN")]
        VietNam = 704,

        [Description("Virgin Islands (British)")]
        [Country2ISOCode("VG")]
        BritishVirginIslands = 092,

        [Description("Virgin Islands (U.S.)")]
        [Country2ISOCode("VI")]
        UnitedStatesVirginIslands = 850,

        [Description("Wallis and Futuna")]
        [Country2ISOCode("WF")]
        WallisandFutuna = 876,

        [Description("Western Sahara")]
        [Country2ISOCode("EH")]
        WesternSahara = 732,

        [Country2ISOCode("YE")]
        Yemen = 887,

        [Country2ISOCode("ZM")]
        Zambia = 894,

        [Country2ISOCode("ZW")]
        Zimbabwe = 716,

        Others = 1000
    }
}
