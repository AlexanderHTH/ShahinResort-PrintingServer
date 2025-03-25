using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Seeders;
internal class PrintServerSeeder(Print_DBContext dbContext,
                                 User_DBContext user_DBContext,
                                 ITQLogger<PrintServerSeeder> logger,
                                 ICountryRepository countriesRepo,
                                 IClientRepository clientRepo,
                                 IPrinterRepository printerRepo,
                                 IPasswordHasher<AppUser> passwordHasher) : IPrintServerSeeder
{
    private async Task SeedDefaultCountriesAsync(AppUser user, CancellationToken cancellationToken)
    {
        var countries = new List<Country>
        {
            new(user.Id, "AF", "Afghanistan", "أفغانستان", "Afghan", "أفغانستاني"),
            new(user.Id, "AL", "Albania", "ألبانيا", "Albania", "ألباني"),
            new(user.Id, "AX", "Aland Islands", "جزر آلاند", "Aland Islander", "آلاندي"),
            new(user.Id, "DZ", "Algeria", "الجزائر", "Algeria", "جزائري"),
            new(user.Id, "AS", "American Samoa", "ساموا-الأمريكي", "American Samoa", "أمريكي سامواني"),
            new(user.Id, "AD", "Andorra", "أندورا", "Andorra", "أندوري"),
            new(user.Id, "AO", "An,la", "أنغولا", "An,la", "أنقولي"),
            new(user.Id, "AI", "Anguilla", "أنغويلا", "Anguilla", "أنغويلي"),
            new(user.Id, "AQ", "Antarctica", "أنتاركتيكا", "Antarctica", "أنتاركتيكي"),
            new(user.Id, "AG", "Antigua and Barbuda", "أنتيغوا وبربودا", "Antigua", "بربودي"),
            new(user.Id, "AR", "Argentina", "الأرجنتين", "Argentinia", "أرجنتيني"),
            new(user.Id, "AM", "Armenia", "أرمينيا", "Armenia", "أرميني"),
            new(user.Id, "AW", "Aruba", "أروبه", "Aruba", "أوروبهيني"),
            new(user.Id, "AU", "Australia", "أستراليا", "Australia", "أسترالي"),
            new(user.Id, "AT", "Austria", "النمسا", "Austria", "نمساوي"),
            new(user.Id, "AZ", "Azerbaija", "أذربيجان", "Azerbaijani", "أذربيجاني"),
            new(user.Id, "BS", "Bahamas", "الباهاماس", "Bahamia", "باهاميسي"),
            new(user.Id, "BH", "Bahrai", "البحرين", "Bahraini", "بحريني"),
            new(user.Id, "BD", "Bangladesh", "بنغلاديش", "Bangladeshi", "بنغلاديشي"),
            new(user.Id, "BB", "Barbados", "بربادوس", "Barbadia", "بربادوسي"),
            new(user.Id, "BY", "Belarus", "روسيا البيضاء", "Belarusia", "روسي"),
            new(user.Id, "BE", "Belgium", "بلجيكا", "Belgia", "بلجيكي"),
            new(user.Id, "BZ", "Belize", "بيليز", "Belizea", "بيليزي"),
            new(user.Id, "BJ", "Beni", "بنين", "Beninese", "بنيني"),
            new(user.Id, "BL", "Saint Barthelemy", "سان بارتيلمي", "Saint Barthelmia", "سان بارتيلمي"),
            new(user.Id, "BM", "Bermuda", "جزر برمودا", "Bermuda", "برمودي"),
            new(user.Id, "BT", "Bhuta", "بوتان", "Bhutanese", "بوتاني"),
            new(user.Id, "BO", "Bolivia", "بوليفيا", "Bolivia", "بوليفي"),
            new(user.Id, "BA", "Bosnia and Herze,vina", "البوسنة و الهرسك", "Bosnian / Herze,vinia", "بوسني/هرسكي"),
            new(user.Id, "BW", "Botswana", "بوتسوانا", "Botswana", "بوتسواني"),
            new(user.Id, "BV", "Bouvet Island", "جزيرة بوفيه", "Bouvetia", "بوفيهي"),
            new(user.Id, "BR", "Brazil", "البرازيل", "Brazilia", "برازيلي"),
            new(user.Id, "IO", "British Indian Ocean Territory", "إقليم المحيط الهندي البريطاني", "British Indian Ocean Territory", "إقليم المحيط الهندي البريطاني"),
            new(user.Id, "B", "Brunei Darussalam", "بروني", "Bruneia", "بروني"),
            new(user.Id, "BG", "Bulgaria", "بلغاريا", "Bulgaria", "بلغاري"),
            new(user.Id, "BF", "Burkina Faso", "بوركينا فاسو", "Burkinabe", "بوركيني"),
            new(user.Id, "BI", "Burundi", "بوروندي", "Burundia", "بورونيدي"),
            new(user.Id, "KH", "Cambodia", "كمبوديا", "Cambodia", "كمبودي"),
            new(user.Id, "CM", "Cameroo", "كاميرون", "Cameroonia", "كاميروني"),
            new(user.Id, "CA", "Canada", "كندا", "Canadia", "كندي"),
            new(user.Id, "CV", "Cape Verde", "الرأس الأخضر", "Cape Verdea", "الرأس الأخضر"),
            new(user.Id, "KY", "Cayman Islands", "جزر كايمان", "Caymania", "كايماني"),
            new(user.Id, "CF", "Central African Republic", "جمهورية أفريقيا الوسطى", "Central Africa", "أفريقي"),
            new(user.Id, "TD", "Chad", "تشاد", "Chadia", "تشادي"),
            new(user.Id, "CL", "Chile", "شيلي", "Chilea", "شيلي"),
            new(user.Id, "C", "China", "الصين", "Chinese", "صيني"),
            new(user.Id, "CX", "Christmas Island", "جزيرة عيد الميلاد", "Christmas Islander", "جزيرة عيد الميلاد"),
            new(user.Id, "CC", "Cocos (Keeling) Islands", "جزر كوكوس", "Cocos Islander", "جزر كوكوس"),
            new(user.Id, "CO", "Colombia", "كولومبيا", "Colombia", "كولومبي"),
            new(user.Id, "KM", "Comoros", "جزر القمر", "Comoria", "جزر القمر"),
            new(user.Id, "CG", "Con,", "الكونغو", "Con,lese", "كونغي"),
            new(user.Id, "CK", "Cook Islands", "جزر كوك", "Cook Islander", "جزر كوك"),
            new(user.Id, "CR", "Costa Rica", "كوستاريكا", "Costa Rica", "كوستاريكي"),
            new(user.Id, "HR", "Croatia", "كرواتيا", "Croatia", "كوراتي"),
            new(user.Id, "CU", "Cuba", "كوبا", "Cuba", "كوبي"),
            new(user.Id, "CY", "Cyprus", "قبرص", "Cypriot", "قبرصي"),
            new(user.Id, "CW", "Curaçao", "كوراساو", "Curacia", "كوراساوي"),
            new(user.Id, "CZ", "Czech Republic", "الجمهورية التشيكية", "Czech", "تشيكي"),
            new(user.Id, "DK", "Denmark", "الدانمارك", "Danish", "دنماركي"),
            new(user.Id, "DJ", "Djibouti", "جيبوتي", "Djiboutia", "جيبوتي"),
            new(user.Id, "DM", "Dominica", "دومينيكا", "Dominica", "دومينيكي"),
            new(user.Id, "DO", "Dominican Republic", "الجمهورية الدومينيكية", "Dominica", "دومينيكي"),
            new(user.Id, "EC", "Ecuador", "إكوادور", "Ecuadoria", "إكوادوري"),
            new(user.Id, "EG", "Egypt", "مصر", "Egyptia", "مصري"),
            new(user.Id, "SV", "El Salvador", "إلسلفادور", "Salvadora", "سلفادوري"),
            new(user.Id, "GQ", "Equatorial Guinea", "غينيا الاستوائي", "Equatorial Guinea", "غيني"),
            new(user.Id, "ER", "Eritrea", "إريتريا", "Eritrea", "إريتيري"),
            new(user.Id, "EE", "Estonia", "استونيا", "Estonia", "استوني"),
            new(user.Id, "ET", "Ethiopia", "أثيوبيا", "Ethiopia", "أثيوبي"),
            new(user.Id, "FK", "Falkland Islands (Malvinas)", "جزر فوكلاند", "Falkland Islander", "فوكلاندي"),
            new(user.Id, "FO", "Faroe Islands", "جزر فارو", "Faroese", "جزر فارو"),
            new(user.Id, "FJ", "Fiji", "فيجي", "Fijia", "فيجي"),
            new(user.Id, "FI", "Finland", "فنلندا", "Finnish", "فنلندي"),
            new(user.Id, "FR", "France", "فرنسا", "French", "فرنسي"),
            new(user.Id, "GF", "French Guiana", "غويانا الفرنسية", "French Guianese", "غويانا الفرنسية"),
            new(user.Id, "PF", "French Polynesia", "بولينيزيا الفرنسية", "French Polynesia", "بولينيزيي"),
            new(user.Id, "TF", "French Southern and Antarctic Lands", "أراض فرنسية جنوبية وأنتارتيكية", "French", "أراض فرنسية جنوبية وأنتارتيكية"),
            new(user.Id, "GA", "Gabo", "الغابون", "Gabonese", "غابوني"),
            new(user.Id, "GM", "Gambia", "غامبيا", "Gambia", "غامبي"),
            new(user.Id, "GE", "Georgia", "جيورجيا", "Georgia", "جيورجي"),
            new(user.Id, "DE", "Germany", "ألمانيا", "Germa", "ألماني"),
            new(user.Id, "GH", "Ghana", "غانا", "Ghanaia", "غاني"),
            new(user.Id, "GI", "Gibraltar", "جبل طارق", "Gibraltar", "جبل طارق"),
            new(user.Id, "GG", "Guernsey", "غيرنزي", "Guernsia", "غيرنزي"),
            new(user.Id, "GR", "Greece", "اليونان", "Greek", "يوناني"),
            new(user.Id, "GL", "Greenland", "جرينلاند", "Greenlandic", "جرينلاندي"),
            new(user.Id, "GD", "Grenada", "غرينادا", "Grenadia", "غرينادي"),
            new(user.Id, "GP", "Guadeloupe", "جزر جوادلوب", "Guadeloupe", "جزر جوادلوب"),
            new(user.Id, "GU", "Guam", "جوام", "Guamania", "جوامي"),
            new(user.Id, "GT", "Guatemala", "غواتيمال", "Guatemala", "غواتيمالي"),
            new(user.Id, "G", "Guinea", "غينيا", "Guinea", "غيني"),
            new(user.Id, "GW", "Guinea-Bissau", "غينيا-بيساو", "Guinea-Bissaua", "غيني"),
            new(user.Id, "GY", "Guyana", "غيانا", "Guyanese", "غياني"),
            new(user.Id, "HT", "Haiti", "هايتي", "Haitia", "هايتي"),
            new(user.Id, "HM", "Heard and Mc Donald Islands", "جزيرة هيرد وجزر ماكدونالد", "Heard and Mc Donald Islanders", "جزيرة هيرد وجزر ماكدونالد"),
            new(user.Id, "H", "Honduras", "هندوراس", "Hondura", "هندوراسي"),
            new(user.Id, "HK", "Hong Kong", "هونغ كونغ", "Hongkongese", "هونغ كونغي"),
            new(user.Id, "HU", "Hungary", "المجر", "Hungaria", "مجري"),
            new(user.Id, "IS", "Iceland", "آيسلندا", "Icelandic", "آيسلندي"),
            new(user.Id, "I", "India", "الهند", "India", "هندي"),
            new(user.Id, "IM", "Isle of Ma", "جزيرة مان", "Manx", "ماني"),
            new(user.Id, "ID", "Indonesia", "أندونيسيا", "Indonesia", "أندونيسيي"),
            new(user.Id, "IR", "Ira", "إيران", "Irania", "إيراني"),
            new(user.Id, "IQ", "Iraq", "العراق", "Iraqi", "عراقي"),
            new(user.Id, "IE", "Ireland", "إيرلندا", "Irish", "إيرلندي"),
            new(user.Id, "IL", "Israel", "إسرائيل", "Israeli", "إسرائيلي"),
            new(user.Id, "IT", "Italy", "إيطاليا", "Italia", "إيطالي"),
            new(user.Id, "CI", "Ivory Coast", "ساحل العاج", "Ivory Coastia", "ساحل العاج"),
            new(user.Id, "JE", "Jersey", "جيرزي", "Jersia", "جيرزي"),
            new(user.Id, "JM", "Jamaica", "جمايكا", "Jamaica", "جمايكي"),
            new(user.Id, "JP", "Japa", "اليابان", "Japanese", "ياباني"),
            new(user.Id, "JO", "Jorda", "الأردن", "Jordania", "أردني"),
            new(user.Id, "KZ", "Kazakhsta", "كازاخستان", "Kazakh", "كازاخستاني"),
            new(user.Id, "KE", "Kenya", "كينيا", "Kenya", "كيني"),
            new(user.Id, "KI", "Kiribati", "كيريباتي", "I-Kiribati", "كيريباتي"),
            new(user.Id, "KP", "Korea(North Korea)", "كوريا الشمالية", "North Korea", "كوري"),
            new(user.Id, "KR", "Korea(South Korea)", "كوريا الجنوبية", "South Korea", "كوري"),
            new(user.Id, "XK", "Kosovo", "كوسوفو", "Kosovar", "كوسيفي"),
            new(user.Id, "KW", "Kuwait", "الكويت", "Kuwaiti", "كويتي"),
            new(user.Id, "KG", "Kyrgyzsta", "قيرغيزستان", "Kyrgyzstani", "قيرغيزستاني"),
            new(user.Id, "LA", "Lao PDR", "لاوس", "Laotia", "لاوسي"),
            new(user.Id, "LV", "Latvia", "لاتفيا", "Latvia", "لاتيفي"),
            new(user.Id, "LB", "Lebano", "لبنان", "Lebanese", "لبناني"),
            new(user.Id, "LS", "Lesotho", "ليسوتو", "Basotho", "ليوسيتي"),
            new(user.Id, "LR", "Liberia", "ليبيريا", "Liberia", "ليبيري"),
            new(user.Id, "LY", "Libya", "ليبيا", "Libya", "ليبي"),
            new(user.Id, "LI", "Liechtenstei", "ليختنشتين", "Liechtenstei", "ليختنشتيني"),
            new(user.Id, "LT", "Lithuania", "لتوانيا", "Lithuania", "لتوانيي"),
            new(user.Id, "LU", "Luxembourg", "لوكسمبورغ", "Luxembourger", "لوكسمبورغي"),
            new(user.Id, "LK", "Sri Lanka", "سريلانكا", "Sri Lankia", "سريلانكي"),
            new(user.Id, "MO", "Macau", "ماكاو", "Macanese", "ماكاوي"),
            new(user.Id, "MK", "Macedonia", "مقدونيا", "Macedonia", "مقدوني"),
            new(user.Id, "MG", "Madagascar", "مدغشقر", "Malagasy", "مدغشقري"),
            new(user.Id, "MW", "Malawi", "مالاوي", "Malawia", "مالاوي"),
            new(user.Id, "MY", "Malaysia", "ماليزيا", "Malaysia", "ماليزي"),
            new(user.Id, "MV", "Maldives", "المالديف", "Maldivia", "مالديفي"),
            new(user.Id, "ML", "Mali", "مالي", "Malia", "مالي"),
            new(user.Id, "MT", "Malta", "مالطا", "Maltese", "مالطي"),
            new(user.Id, "MH", "Marshall Islands", "جزر مارشال", "Marshallese", "مارشالي"),
            new(user.Id, "MQ", "Martinique", "مارتينيك", "Martiniquais", "مارتينيكي"),
            new(user.Id, "MR", "Mauritania", "موريتانيا", "Mauritania", "موريتانيي"),
            new(user.Id, "MU", "Mauritius", "موريشيوس", "Mauritia", "موريشيوسي"),
            new(user.Id, "YT", "Mayotte", "مايوت", "Mahora", "مايوتي"),
            new(user.Id, "MX", "Mexico", "المكسيك", "Mexica", "مكسيكي"),
            new(user.Id, "FM", "Micronesia", "مايكرونيزيا", "Micronesia", "مايكرونيزيي"),
            new(user.Id, "MD", "Moldova", "مولدافيا", "Moldova", "مولديفي"),
            new(user.Id, "MC", "Monaco", "موناكو", "Monaca", "مونيكي"),
            new(user.Id, "M", "Mon,lia", "منغوليا", "Mon,lia", "منغولي"),
            new(user.Id, "ME", "Montenegro", "الجبل الأسود", "Montenegri", "الجبل الأسود"),
            new(user.Id, "MS", "Montserrat", "مونتسيرات", "Montserratia", "مونتسيراتي"),
            new(user.Id, "MA", "Morocco", "المغرب", "Morocca", "مغربي"),
            new(user.Id, "MZ", "Mozambique", "موزمبيق", "Mozambica", "موزمبيقي"),
            new(user.Id, "MM", "Myanmar", "ميانمار", "Myanmaria", "ميانماري"),
            new(user.Id, "NA", "Namibia", "ناميبيا", "Namibia", "ناميبي"),
            new(user.Id, "NR", "Nauru", "نورو", "Naurua", "نوري"),
            new(user.Id, "NP", "Nepal", "نيبال", "Nepalese", "نيبالي"),
            new(user.Id, "NL", "Netherlands", "هولندا", "Dutch", "هولندي"),
            new(user.Id, "A", "Netherlands Antilles", "جزر الأنتيل الهولندي", "Dutch Antilier", "هولندي"),
            new(user.Id, "NC", "New Caledonia", "كاليدونيا الجديدة", "New Caledonia", "كاليدوني"),
            new(user.Id, "NZ", "New Zealand", "نيوزيلندا", "New Zealander", "نيوزيلندي"),
            new(user.Id, "NI", "Nicaragua", "نيكاراجوا", "Nicaragua", "نيكاراجوي"),
            new(user.Id, "NE", "Niger", "النيجر", "Nigerie", "نيجيري"),
            new(user.Id, "NG", "Nigeria", "نيجيريا", "Nigeria", "نيجيري"),
            new(user.Id, "NU", "Niue", "ني", "Niuea", "ني"),
            new(user.Id, "NF", "Norfolk Island", "جزيرة نورفولك", "Norfolk Islander", "نورفوليكي"),
            new(user.Id, "MP", "Northern Mariana Islands", "جزر ماريانا الشمالية", "Northern Mariana", "ماريني"),
            new(user.Id, "NO", "Norway", "النرويج", "Norwegia", "نرويجي"),
            new(user.Id, "OM", "Oma", "عمان", "Omani", "عماني"),
            new(user.Id, "PK", "Pakista", "باكستان", "Pakistani", "باكستاني"),
            new(user.Id, "PW", "Palau", "بالاو", "Palaua", "بالاوي"),
            new(user.Id, "PS", "Palestine", "فلسطين", "Palestinia", "فلسطيني"),
            new(user.Id, "PA", "Panama", "بنما", "Panamania", "بنمي"),
            new(user.Id, "PG", "Papua New Guinea", "بابوا غينيا الجديدة", "Papua New Guinea", "بابوي"),
            new(user.Id, "PY", "Paraguay", "باراغواي", "Paraguaya", "بارغاوي"),
            new(user.Id, "PE", "Peru", "بيرو", "Peruvia", "بيري"),
            new(user.Id, "PH", "Philippines", "الفليبين", "Filipino", "فلبيني"),
            new(user.Id, "P", "Pitcair", "بيتكيرن", "Pitcairn Islander", "بيتكيرني"),
            new(user.Id, "PL", "Poland", "بولونيا", "Polish", "بوليني"),
            new(user.Id, "PT", "Portugal", "البرتغال", "Portuguese", "برتغالي"),
            new(user.Id, "PR", "Puerto Rico", "بورتو ريكو", "Puerto Rica", "بورتي"),
            new(user.Id, "QA", "Qatar", "قطر", "Qatari", "قطري"),
            new(user.Id, "RE", "Reunion Island", "ريونيون", "Reunionese", "ريونيوني"),
            new(user.Id, "RO", "Romania", "رومانيا", "Romania", "روماني"),
            new(user.Id, "RU", "Russia", "روسيا", "Russia", "روسي"),
            new(user.Id, "RW", "Rwanda", "رواندا", "Rwanda", "رواندا"),
            new(user.Id, "K", "Saint Kitts and Nevis", "سانت كيتس ونيفس,", "Kittitian/Nevisia", "سانت كيتس ونيفس"),
            new(user.Id, "MF", "Saint Martin (French part)", "ساينت مارتن فرنسي", "St. Martian(French)", "ساينت مارتني فرنسي"),
            new(user.Id, "SX", "Sint Maarten (Dutch part)", "ساينت مارتن هولندي", "St. Martian(Dutch)", "ساينت مارتني هولندي"),
            new(user.Id, "LC", "Saint Pierre and Miquelo", "سان بيير وميكلون", "St. Pierre and Miquelo", "سان بيير وميكلوني"),
            new(user.Id, "VC", "Saint Vincent and the Grenadines", "سانت فنسنت وجزر غرينادين", "Saint Vincent and the Grenadines", "سانت فنسنت وجزر غرينادين"),
            new(user.Id, "WS", "Samoa", "ساموا", "Samoa", "ساموي"),
            new(user.Id, "SM", "San Marino", "سان مارينو", "Sammarinese", "ماريني"),
            new(user.Id, "ST", "Sao Tome and Principe", "ساو تومي وبرينسيبي", "Sao Tomea", "ساو تومي وبرينسيبي"),
            new(user.Id, "SA", "Saudi Arabia", "المملكة العربية السعودية", "Saudi Arabia", "سعودي"),
            new(user.Id, "S", "Senegal", "السنغال", "Senegalese", "سنغالي"),
            new(user.Id, "RS", "Serbia", "صربيا", "Serbia", "صربي"),
            new(user.Id, "SC", "Seychelles", "سيشيل", "Seychellois", "سيشيلي"),
            new(user.Id, "SL", "Sierra Leone", "سيراليون", "Sierra Leonea", "سيراليوني"),
            new(user.Id, "SG", "Singapore", "سنغافورة", "Singaporea", "سنغافوري"),
            new(user.Id, "SK", "Slovakia", "سلوفاكيا", "Slovak", "سولفاكي"),
            new(user.Id, "SI", "Slovenia", "سلوفينيا", "Slovenia", "سولفيني"),
            new(user.Id, "SB", "Solomon Islands", "جزر سليمان", "Solomon Island", "جزر سليمان"),
            new(user.Id, "SO", "Somalia", "الصومال", "Somali", "صومالي"),
            new(user.Id, "ZA", "South Africa", "جنوب أفريقيا", "South Africa", "أفريقي"),
            new(user.Id, "GS", "South Georgia and the South Sandwich", "المنطقة القطبية الجنوبية", "South Georgia and the South Sandwich", "لمنطقة القطبية الجنوبية"),
            new(user.Id, "SS", "South Suda", "السودان الجنوبي", "South Sudanese", "سوادني جنوبي"),
            new(user.Id, "ES", "Spai", "إسبانيا", "Spanish", "إسباني"),
            new(user.Id, "SH", "Saint Helena", "سانت هيلانة", "St. Helenia", "هيلاني"),
            new(user.Id, "SD", "Suda", "السودان", "Sudanese", "سوداني"),
            new(user.Id, "SR", "Suriname", "سورينام", "Surinamese", "سورينامي"),
            new(user.Id, "SJ", "Svalbard and Jan Maye", "سفالبارد ويان ماين", "Svalbardian/Jan Mayenia", "سفالبارد ويان ماين"),
            new(user.Id, "SZ", "Swaziland", "سوازيلند", "Swazi", "سوازيلندي"),
            new(user.Id, "SE", "Swede", "السويد", "Swedish", "سويدي"),
            new(user.Id, "CH", "Switzerland", "سويسرا", "Swiss", "سويسري"),
            new(user.Id, "SY", "Syria", "سوريا", "Syria", "سوري"),
            new(user.Id, "TW", "Taiwa", "تايوان", "Taiwanese", "تايواني"),
            new(user.Id, "TJ", "Tajikista", "طاجيكستان", "Tajikistani", "طاجيكستاني"),
            new(user.Id, "TZ", "Tanzania", "تنزانيا", "Tanzania", "تنزانيي"),
            new(user.Id, "TH", "Thailand", "تايلندا", "Thai", "تايلندي"),
            new(user.Id, "TL", "Timor-Leste", "تيمور الشرقية", "Timor-Lestia", "تيموري"),
            new(user.Id, "TG", "To,", "توغو", "To,lese", "توغي"),
            new(user.Id, "TK", "Tokelau", "توكيلاو", "Tokelaia", "توكيلاوي"),
            new(user.Id, "TO", "Tonga", "تونغا", "Tonga", "تونغي"),
            new(user.Id, "TT", "Trinidad and Toba,", "ترينيداد وتوباغو", "Trinidadian/Toba,nia", "ترينيداد وتوباغو"),
            new(user.Id, "T", "Tunisia", "تونس", "Tunisia", "تونسي"),
            new(user.Id, "TR", "Turkey", "تركيا", "Turkish", "تركي"),
            new(user.Id, "TM", "Turkmenista", "تركمانستان", "Turkme", "تركمانستاني"),
            new(user.Id, "TC", "Turks and Caicos Islands", "جزر توركس وكايكوس", "Turks and Caicos Islands", "جزر توركس وكايكوس"),
            new(user.Id, "TV", "Tuvalu", "توفالو", "Tuvalua", "توفالي"),
            new(user.Id, "UG", "Uganda", "أوغندا", "Uganda", "أوغندي"),
            new(user.Id, "UA", "Ukraine", "أوكرانيا", "Ukrainia", "أوكراني"),
            new(user.Id, "AE", "United Arab Emirates", "الإمارات العربية المتحدة", "Emirati", "إماراتي"),
            new(user.Id, "GB", "United Kingdom", "المملكة المتحدة", "British", "بريطاني"),
            new(user.Id, "US", "United States", "الولايات المتحدة", "America", "أمريكي"),
            new(user.Id, "UM", "US Minor Outlying Islands", "قائمة الولايات والمناطق الأمريكية", "US Minor Outlying Islander", "أمريكي"),
            new(user.Id, "UY", "Uruguay", "أورغواي", "Uruguaya", "أورغواي"),
            new(user.Id, "UZ", "Uzbekista", "أوزباكستان", "Uzbek", "أوزباكستاني"),
            new(user.Id, "VU", "Vanuatu", "فانواتو", "Vanuatua", "فانواتي"),
            new(user.Id, "VE", "Venezuela", "فنزويلا", "Venezuela", "فنزويلي"),
            new(user.Id, "V", "Vietnam", "فيتنام", "Vietnamese", "فيتنامي"),
            new(user.Id, "VI", "Virgin Islands (U.S.)", "الجزر العذراء الأمريكي", "American Virgin Islander", "أمريكي"),
            new(user.Id, "VA", "Vatican City", "فنزويلا", "Vatica", "فاتيكاني"),
            new(user.Id, "WF", "Wallis and Futuna Islands", "والس وفوتونا", "Wallisian/Futuna", "فوتوني"),
            new(user.Id, "EH", "Western Sahara", "الصحراء الغربية", "Sahrawia", "صحراوي"),
            new(user.Id, "YE", "Yeme", "اليمن", "Yemeni", "يمني"),
            new(user.Id, "ZM", "Zambia", "زامبيا", "Zambia", "زامبياني"),
            new(user.Id, "ZW", "Zimbabwe", "زمبابوي", "Zimbabwea", "زمبابوي"),
        };
        foreach (Country country in countries)
        {
            country.AppUserId = user.Id;
        }
        logger.LogInformation("Seeding Countries ({@count}) data.", countries.Count);
        await countriesRepo.Create(countries, cancellationToken);
    }
    private async Task SeedDefaultClientsAsync(AppUser user, CancellationToken cancellationToken)
    {
        var clients = new List<Client>
        {
            new (user.Id, "Reception1", "192.168.3.11"),
            new (user.Id, "Reception2", "192.168.3.12"),
            new (user.Id, "Reception3", "192.168.3.13"),
            new (user.Id, "Reception4", "192.168.3.14"),
            new (user.Id, "MainServer", "192.168.2.17"),
            new (user.Id, "Sales", "192.168.3.20"),
            new (user.Id, "Test Server", "::1"),
            new (user.Id, "Accountent", "192.168.3.22"),
            new (user.Id, "Ali Omoum LapTop", "192.168.9.132"),
            new (user.Id, "Reception5", "192.168.3.8"),
            new (user.Id, "Reception6", "192.168.3.9"),
            new (user.Id, "Reception7", "192.168.3.10"),
            new (user.Id, "tr", "127.0.0.1"),
            new (user.Id, "ali2", "192.168.10.8"),
            new (user.Id, "Ali _1", "192.168.13.13")
        };
        foreach (Client client in clients)
        {
            client.AppUserId = user.Id;
        }
        logger.LogInformation("Seeding Clients ({@count}) data.", clients.Count);
        await clientRepo.Create(clients, cancellationToken);
    }
    private async Task SeedDefaultPrintersAsync(AppUser user, CancellationToken cancellationToken)
    {
        var printers = new List<Printer>
        {
            new(user.Id, "RONGTA 80mm Series Printer", "192.168.3.145", 500),
            new(user.Id, "إرسال إلى OneNote 2010", "Unknown", 500),
            new(user.Id, "OneNote", "Unknown", 500),
            new(user.Id, "ReceptionPrinter2", "192.168.3.46", 500),
            new(user.Id, "ReceptionPrinter", "192.168.5.68", 500),
            new(user.Id, "Microsoft XPS Document Writer", "Unknown", 500),
            new(user.Id, "Microsoft Print to PDF", "Unknown", 500),
            new(user.Id, "HP Universal Printing PCL 6", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404-M405 UPD PCL 6 (Copy 1)", "Unknown", 500),
            new(user.Id, "Fax", "Unknown", 500),
            new(user.Id, "AnyDesk Printer", "Unknown", 500),
            new(user.Id, "OneNote (redirected 2)", "Unknown", 500),
            new(user.Id, "Fax (redirected 2)", "Unknown", 500),
            new(user.Id, "HP Universal Printing PCL 6 (redirected 2)", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404-M405 UPD PCL 6 (Copy 1) (redirected 2)", "Unknown", 500),
            new(user.Id, "Microsoft XPS Document Writer (redirected 2)", "Unknown", 500),
            new(user.Id, "Microsoft Print to PDF (redirected 2)", "Unknown", 500),
            new(user.Id, "ReceptionPrinter (redirected 2)", "Unknown", 500),
            new(user.Id, "Reception2_RONGTA", "192.168.3.126", 500),
            new(user.Id, "Reception2_HP", "192.168.3.164", 500),
            new(user.Id, "Reception1_ RONGTA", "192.168.3.125", 500),
            new(user.Id, "Reception1_HP", "192.168.3.163", 500) ,
            new(user.Id, "OneNote for Windows 10 (redirected 2)", "Unknown", 500),
            new(user.Id, "Acc_Printer", "192.168.3.168", 500),
            new(user.Id, "Reception3_HP", "192.168.3.178", 500),
            new(user.Id, "Reception3_80mm", "192.168.3.177", 500),
            new(user.Id, "Generic / Text Only", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404-M405 UPD PS", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404dn (192.168.3.164)", "192.168.3.164", 500),
            new(user.Id, "Microsoft XPS Document Writer (redirected 2)", "Unknown", 500),
            new(user.Id, "Fax (redirected 2)", "Unknown", 500),
            new(user.Id, "Microsoft Print to PDF (redirected 2)", "Unknown", 500),
            new(user.Id, "OneNote for Windows 10 (redirected 2)", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404-M405 [B0342D] (redirected 2)", "Unknown", 500),
            new(user.Id, "Reception3_HP", "Unknown", 500),
            new(user.Id, "Reception3_80mm", "Unknown", 500),
            new(user.Id, "Reception2_RONGTA", "Unknown", 500),
            new(user.Id, "Reception2_HP", "192.168.3.164", 500),
            new(user.Id, "Reception1_HP", "192.168.3.163", 500),
            new(user.Id, "Reception1_ RONGTA", "Unknown", 500),
            new(user.Id, "Microsoft XPS Document Writer", "Unknown", 500),
            new(user.Id, "Microsoft Print to PDF", "Unknown", 500),
            new(user.Id, "HP LaserJet Pro M404dn (192.168.3.164)", "192.168.3.164", 500),
            new(user.Id, "HP LaserJet Pro M404-M405 UPD PS", "Unknown", 500)    ,
            new(user.Id, "Generic / Text Only", "Unknown", 500),
            new(user.Id, "Acc_Printer", "Unknown", 500)
        };
        foreach (Printer printer in printers)
        {
            printer.AppUserId = user.Id;
        }
        logger.LogInformation("Seeding Printers ({@count}) data.", printers.Count);
        await printerRepo.Create(printers, cancellationToken);
    }
    private async Task SeedDefaultRolesAsync(CancellationToken cancellationToken)
    {
        List<AppRole> roles = [];
        foreach (string s in UserRoles.Roles)
        {
            AppRole r = new()
            {
                Id = Guid.NewGuid(),
                Name = s,
                NormalizedName = s.ToUpper()
            };
            roles.Add(r);
        }
        logger.LogInformation("Seeding Roles ({@count}) data.", roles.Count);
        user_DBContext.Roles.AddRange(roles);
        await user_DBContext.SaveChangesAsync(cancellationToken);
    }
    private async Task SeedDefaultUsersAsync(CancellationToken cancellationToken)
    {
        List<AppUser> users = [];
        foreach (string s in UserRoles.Roles)
        {
            AppUser user = new()
            {
                Id = Guid.NewGuid(),
                UserName = s,
                Email = AccountSettings.GetEmail(s),
                NormalizedUserName = s.ToUpper(),
                NormalizedEmail = AccountSettings.GetEmail(s).ToUpper(),
                LockoutEnabled = false,
                TokenVersion = 0
            };
            var hashedPassword = passwordHasher.HashPassword(user, AccountSettings.DefaultPassword);
            user.SecurityStamp = user.Id.ToString();
            user.PasswordHash = hashedPassword;
            users.Add(user);
        }
        logger.LogInformation($"Seeding Users ({users.Count}) data.");
        user_DBContext.Users.AddRange(users);
        await user_DBContext.SaveChangesAsync(cancellationToken);
    }
    private async Task SeedDefaultUserRolesAsync(CancellationToken cancellationToken)
    {
        List<IdentityUserRole<Guid>> userRoles = [];
        foreach (string s in UserRoles.Roles)
        {
            var role = user_DBContext.Roles.Single(r => r.Name == s);
            var user = user_DBContext.Users.Single(u => u.UserName == AccountSettings.GetEmail(s));
            userRoles.Add(new IdentityUserRole<Guid>() { UserId = user.Id, RoleId = role.Id });
        }
        logger.LogInformation("Seeding Users-Roles ({@count}) data.", userRoles.Count);
        user_DBContext.UserRoles.AddRange(userRoles);
        await user_DBContext.SaveChangesAsync(cancellationToken);
    }
    private async Task MigrateDataBaseAsync(string msg, DbContext context, IEnumerable<string>? migrations)
    {
        logger.LogWarning(msg);
        await context.Database.MigrateAsync();
        if (migrations != null)
        {
            foreach (string migration in migrations)
            {
                logger.LogInformation($"Migration ({migration}) applied successfully.");
            }
        }
        logger.LogInformation("Migrations applied successfully.");
    }
    private async Task HandleMainDataBase(CancellationToken cancellationToken)
    {
        try
        {
            var connection = dbContext.Database.GetDbConnection();
            var databasename = string.Format("{0}/{1}", connection.DataSource, connection.Database);
            if (!await dbContext.Database.CanConnectAsync())
            {
                await MigrateDataBaseAsync(string.Format("Database ({0}) not found ({1}). Running migrations...", databasename, connection.ConnectionString), dbContext, null);
            }
            logger.LogInformation("Connected Successfully to database ({@databasename})", databasename);
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await MigrateDataBaseAsync("Applying pending migrations: ", dbContext, pendingMigrations);
            }
            else
            {
                logger.LogInformation("Database ({@databasename}) is up to date. No migrations to apply.", databasename);
            }
            if (!await countriesRepo.IsEmpty() && !await clientRepo.IsEmpty() && !await printerRepo.IsEmpty())
            {
                logger.LogInformation("No seeding needed on DataBase ({@databasename}).", databasename);
            }
            else
            {
                logger.LogInformation("Seeding on DataBase ({@databasename}).", databasename);
                var user = user_DBContext.Users.Single(u => u.UserName == AccountSettings.GetEmail(UserRoles.Manager));
                if (await countriesRepo.IsEmpty())
                {
                    await SeedDefaultCountriesAsync(user, cancellationToken);
                }
                if (await clientRepo.IsEmpty())
                {
                    await SeedDefaultClientsAsync(user, cancellationToken);
                }
                if (await printerRepo.IsEmpty())
                {
                    await SeedDefaultPrintersAsync(user, cancellationToken);
                }
                logger.LogInformation("Done seeding on DataBase ({@databasename}).", databasename);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during database migration.");
        }
    }
    private async Task HandleUsersDataBase(CancellationToken cancellationToken)
    {
        try
        {
            var connection = user_DBContext.Database.GetDbConnection();
            var databasename = string.Format("{0}/{1}", connection.DataSource, connection.Database);
            if (!await user_DBContext.Database.CanConnectAsync())
            {
                await MigrateDataBaseAsync(string.Format("Database ({0}) not found ({1}). Running migrations...", databasename, connection.ConnectionString), user_DBContext, null);
            }
            logger.LogInformation("Connected Successfully to database ({@databasename})", databasename);
            var pendingMigrations = await user_DBContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await MigrateDataBaseAsync(string.Format("Applying pending migrations on Database ({0}): ", databasename), user_DBContext, pendingMigrations);
            }
            else
            {
                logger.LogInformation("Database ({@databasename}) is up to date. No migrations to apply.", databasename);
            }
            if (user_DBContext.Roles.Any() && user_DBContext.Users.Any() && user_DBContext.UserRoles.Any())
            {
                logger.LogInformation("No seeding needed on DataBase ({@databasename}).", databasename);
            }
            else
            {
                logger.LogInformation("Seeding on DataBase ({@databasename}).", databasename);
                if (!user_DBContext.Roles.Any())
                {
                    await SeedDefaultRolesAsync(cancellationToken);
                }
                if (!user_DBContext.Users.Any())
                {
                    await SeedDefaultUsersAsync(cancellationToken);
                }
                if (!user_DBContext.UserRoles.Any())
                {
                    await SeedDefaultUserRolesAsync(cancellationToken);
                }
                logger.LogInformation("Done seeding on DataBase ({@databasename}).", databasename);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during database migration.");
        }
    }
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            await HandleUsersDataBase(cancellationToken);
            await HandleMainDataBase(cancellationToken);
        }
    }
}