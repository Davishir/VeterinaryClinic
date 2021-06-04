using System;
using System.Linq;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Data
{
    public class DbInitializer
    {
        public static void Initialize(VeterinaryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any owner.
            if (context.Owners.Any())
            {
                return;   // DB has been seeded
            }

            var owners = new Owner[]
            {
            new Owner{Fio="Carson Alexander",Address="330828, Костромская область, город Ступино, ул. Будапештсткая, 63", PhoneNumber="+7 (922) 487-4243"},
            new Owner{Fio="Meredith Alonso",Address="420163, Кемеровская область, город Клин, проезд Чехова, 69", PhoneNumber="8-800-576-7484"},
            new Owner{Fio="Arturo Anand",Address="971691, Псковская область, город Видное, проезд 1905 года, 98",PhoneNumber="+7 (922) 137-4746"},
            new Owner{Fio="Gytis Barzdukas",Address="721745, Томская область, город Кашира, пл. Балканская, 65",PhoneNumber="+7 (922) 499-7494"},
            new Owner{Fio="Yan Li",Address="790721, Вологодская область, город Москва, наб. Космонавтов, 23",PhoneNumber="+7 (922) 031-7324"},
            new Owner{Fio="Peggy Justice",Address="650947, Кемеровская область, город Серпухов, спуск Гоголя, 45",PhoneNumber="8-800-629-1243"},
            new Owner{Fio="Laura Norman",Address="572545, Самарская область, город Пушкино, наб. Чехова, 66",PhoneNumber="8-800-923-7337"},
            new Owner{Fio="Nino Olivetto",Address="394952, Кемеровская область, город Лотошино, бульвар Косиора, 85",PhoneNumber="+7 (922) 302-3067"},
            new Owner{Fio="Павлов Герман Ильич",Address="г.Новокиевский Увал, ул. Советская, дом 2, квартира 70",PhoneNumber="8 (796) 524-58-95"},
            new Owner{Fio="Пономарёв Карл Феликсович",Address="г.Междуреченск, ул. Пушкина, дом 13, квартира 2",PhoneNumber="8 (607) 797-59-62"},
            new Owner{Fio="Афанасьев Семён Рудольфович",Address="г.Алатырь, ул. Заречная, дом 79, квартира 130",PhoneNumber="8 (579) 398-24-40"},
            new Owner{Fio="Фомин Иннокентий Григорьевич",Address="г.Черемисиново, ул. Хрущёва, дом 86, квартира 66",PhoneNumber="8 (699) 498-92-11"},
            new Owner{Fio="Данилов Вячеслав Антонович",Address="г.Шушенское, ул. Маяковского, дом 47, квартира 94",PhoneNumber="8 (353) 496-93-81"},
            new Owner{Fio="Ерошенко Кира Антоновна",Address="394006, г. Дергачи, ул. Горбольницы тер, дом 21, квартира 494",PhoneNumber="+7 (951) 014-51-51"},
            new Owner{Fio="Демьянченко Алина Геннадиевна",Address="692411, г. Курах, ул. Двинцев, дом 151, квартира 338",PhoneNumber="+7 (978) 976-03-22"},
            new Owner{Fio="Репина Ганна Валерьевна",Address="606657, г. Кумены, ул. Военная Горка (5-я Линия) пер, дом 12, квартира 982",PhoneNumber="+7 (930) 626-68-66"},
            new Owner{Fio="Михайлова Ксения Закировна",Address="119590, г. Усть-Кишерть, ул. Евгения Середкина, дом 45, квартира 519",PhoneNumber="+7 (961) 065-81-70"},
            new Owner{Fio="Быстрова Аделаида Федоровна",Address="429341, г. Троицк, ул. Новые Сады 1-я, дом 138, квартира 401",PhoneNumber="+7 (911) 985-38-11"},
            new Owner{Fio="Борисова Таисия Игоревна",Address="403844, г. Чикола, ул. Расстанный пер, дом 91, квартира 954",PhoneNumber="+7 (910) 362-88-05"},
            new Owner{Fio="Байков Иларион Антонович",Address="214961, г. Ленинск-Кузнецкий, ул. Ельцовка, дом 192, квартира 60",PhoneNumber="+7 (978) 921-80-67"},
            new Owner{Fio="Смирнов Иннокентий Данилович",Address="413617, г. Завьялиха, ул. Белоусова, дом 32, квартира 704",PhoneNumber="+7 (999) 574-94-70"},
            new Owner{Fio="Садовничий Феликс Кириллович",Address="633453, г. Любим, ул. Кирпичная, дом 38, квартира 364",PhoneNumber="+7 (974) 681-60-62"},
            new Owner{Fio="Титов Геннадий Денисович",Address="396315, г. Киргиз-Мияки, ул. Краснопрудная, дом 184, квартира 841",PhoneNumber="+7 (910) 275-51-54"},
            new Owner{Fio="Краснов Матвей Русланович",Address="102207, г. Степное, ул. Досфлота проезд, дом 84, квартира 251",PhoneNumber="+7 (929) 395-31-09"},
            new Owner{Fio="Антонов Дмитрий Федорович",Address="171914, г. Шаранга, ул. 10-летия Октября, дом 158, квартира 453",PhoneNumber="+7 (927) 791-18-67"},
            new Owner{Fio="Чуракова Елизавета Александровна",Address="Рефтенский ул Гагарина  дом 20 квартира 40",PhoneNumber="+7 (912) 361-86-11"}
            };
            foreach (Owner o in owners)
            {
                context.Owners.Add(o);
            }
            context.SaveChanges();

            var animals = new Animal[]
            {
            new Animal{NickName="Катерина Катериновна",Kind="Кошка",Breed="Регдолл",DateOfBirth=DateTime.Parse("2016-12-25"),Sex="Жен",Color="серый",Length="35",Weight="2 кг"},
            new Animal{NickName="Жастин",Kind="Собака",Breed="Йоркширский терьер",DateOfBirth=DateTime.Parse("2015-10-15"),Sex="Муж",Color="Золотисто-чёрный",Length="20 ",Weight="3,2 кг"},
            new Animal{NickName="Лестор",Kind="Собака",Breed="Чихуахуа",DateOfBirth=DateTime.Parse("2018-02-14"),Sex="Муж",Color="Кремовый",Length="18",Weight="2 кг"},
            new Animal{NickName="Джаго",Kind="Собака",Breed="Такса",DateOfBirth=DateTime.Parse("2019-09-03"),Sex="Муж",Color="Чёрный с рыжими подпалинами",Length="38 ",Weight="8 кг"},
            new Animal{NickName="Лео",Kind="Кошка",Breed="Оцикет",DateOfBirth=DateTime.Parse("2016-11-28"),Sex="Муж",Color="Белый",Length="18",Weight="2,2 кг"},
            new Animal{NickName="Илай",Kind="Собака",Breed="Немецкая овчарка",DateOfBirth=DateTime.Parse("2017-04-08"),Sex="Муж",Color="Чёрный с рыжими подпалинами",Length="63",Weight="31 кг"},
            new Animal{NickName="Кира",Kind="Хомяк",Breed="Джунгарский хомяк",DateOfBirth=DateTime.Parse("2020-01-09"),Sex="Жен",Color="Коричневый",Length="6",Weight="0,03"},
            new Animal{NickName="Шерри",Kind="Кошка",Breed="Бурманская кошка",DateOfBirth=DateTime.Parse("2016-10-25"),Sex="Жен",Color="Рыжий",Length="32",Weight="4,5"},
            new Animal{NickName="Джейси",Kind="Кролик",Breed="Рекс",DateOfBirth=DateTime.Parse("2018-09-08"),Sex="Жен",Color="Черный",Length="11",Weight="8"},
            new Animal{NickName="Ильда",Kind="Кошка",Breed="Сибирская кошка",DateOfBirth=DateTime.Parse("2017-10-16"),Sex="Жен",Color="Серебоистый",Length="29",Weight="3,5"},
            new Animal{NickName="Мальта",Kind="Шиншила",Breed="Бархатный бело-розовый",DateOfBirth=DateTime.Parse("2015-07-30"),Sex="Жен",Color="Бело-розовый",Length="17",Weight="6"},
            new Animal{NickName="Авалон",Kind="Кошка",Breed="Тонкинская кошка",DateOfBirth=DateTime.Parse("2014-02-28"),Sex="Муж",Color="Белый",Length="31",Weight="4 кг"},
            new Animal{NickName="Рыся",Kind="Кошка",Breed="Австралийский мист",DateOfBirth=DateTime.Parse("2015-03-07"),Sex="Жен",Color="Кремовый",Length="32",Weight="2,5"},
            new Animal{NickName="Эмилия",Kind="Попугай",Breed="Лори",DateOfBirth=DateTime.Parse("2013-11-30"),Sex="Жен",Color="Зеленый с желтым",Length="11",Weight="0,12 кг"},
            new Animal{NickName="Дейзи",Kind="Кролик",Breed="Карликовый баран",DateOfBirth=DateTime.Parse("2019-10-22"),Sex="Жен",Color="Серебристый",Length="13",Weight="7 кг"},
            new Animal{NickName="Плуто",Kind="Собака",Breed="Лабрадор-ретривер",DateOfBirth=DateTime.Parse("2018-09-04"),Sex="Муж",Color="Светло-золотой",Length="56",Weight="32 кг"},
            new Animal{NickName="Ярик",Kind="Собака",Breed="Хаски",DateOfBirth=DateTime.Parse("2016-12-21"),Sex="Муж",Color="Черный",Length="55",Weight="21 кг"},
            new Animal{NickName="Арамис",Kind="Попугай",Breed="Волнистые попугайчики",DateOfBirth=DateTime.Parse("2016-05-20"),Sex="Муж",Color="Голубой с белым",Length="11",Weight="0,04 кг"},
            new Animal{NickName="Заг",Kind="Собака",Breed="Джек-рассел-терьер",DateOfBirth=DateTime.Parse("2019-06-05"),Sex="Муж",Color="Пиболд",Length="27",Weight="6,5 кг"},
            new Animal{NickName="Раф",Kind="Собака",Breed="Немецкий шпиц",DateOfBirth=DateTime.Parse("2016-08-14"),Sex="Муж",Color="чёрный с рыжими подпалинами",Length="35",Weight="8 кг"},
            new Animal{NickName="Грем",Kind="Кошка",Breed="Русская голубая",DateOfBirth=DateTime.Parse("2020-02-05"),Sex="Муж",Color="Серый",Length="27",Weight="2,6 кг"},
            new Animal{NickName="Бумер",Kind="Хомяк",Breed="Обыкновенный хомяк",DateOfBirth=DateTime.Parse("2019-08-12"),Sex="Муж",Color="Черный",Length="7",Weight="0,02"},
            new Animal{NickName="Люкс",Kind="Кролик",Breed="Гермелин",DateOfBirth=DateTime.Parse("2016-03-22"),Sex="Муж",Color="Голубой",Length="16",Weight="6 кг"},
            new Animal{NickName="Виктория",Kind="Кошка",Breed="Бирманская кошка ",DateOfBirth=DateTime.Parse("2016-12-06"),Sex="Жен",Color="Серый",Length="29",Weight="2,6 кг"},
            new Animal{NickName="Норман",Kind="Кролик",Breed="Советская шиншилла",DateOfBirth=DateTime.Parse("2020-04-12"),Sex="Муж",Color="Серый",Length="13",Weight="5 кг"},
            new Animal{NickName="Герда",Kind="Собака",Breed="Бассет-хаунд",DateOfBirth=DateTime.Parse("2010-09-18"),Sex="Жен",Color="Рыжий с черным",Length="30 см",Weight="24 кг"},
            new Animal{NickName="Пух",Kind="Хомяк",Breed="Хомяк Кэмпбелла",DateOfBirth=DateTime.Parse("2015-08-08"),Sex="Муж",Color="Кремовый",Length="6,5",Weight="0,03"},
            new Animal{NickName="Хенк",Kind="Хомяк",Breed="Хомяк Роборовского",DateOfBirth=DateTime.Parse("2019-05-24"),Sex="Муж",Color="Рыжий",Length="5",Weight="0,025"},
            new Animal{NickName="Хеппи",Kind="Собака",Breed="Поеранский шпиц",DateOfBirth=DateTime.Parse("2016-06-15"),Sex="Муж",Color="Рыжий",Length="30",Weight="1,8 кг"},
            new Animal{NickName="Никуся",Kind="Кошка",Breed="Норвежская лесная кошка",DateOfBirth=DateTime.Parse("2016-11-20"),Sex="Жен",Color="Белый",Length="29",Weight="1,9 кг"}
            };
            foreach (Animal a in animals)
            {
                context.Animals.Add(a);
            }
            context.SaveChanges();

            var visits = new Visit[]
            {
            new Visit{AnimalID=26,OwnerID=1,Complaints="Хромает, появление судорок", Diagnosis="Ушиб сустава", AttendinDoctor="Ортопед Петров Петр Петрович", Price= 1800, Dateofvisit=DateTime.Parse("2021-05-21"), Duration="25 мин"},
            new Visit{AnimalID=2,OwnerID=2,Complaints="Вялость, малоподвижность", Diagnosis="Анемия.", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 500 , Dateofvisit=DateTime.Parse("2021-05-22"), Duration="20 мин" },
            new Visit{AnimalID=3,OwnerID=3,Complaints="Ходит кругами", Diagnosis="Тревожность", AttendinDoctor="Невролог Синицына Анна Александровна", Price=650 , Dateofvisit=DateTime.Parse("2021-05-22"), Duration="10 мин"},
            new Visit{AnimalID=4,OwnerID=4,Complaints="Одышка, тяжелое дыхание, хрипы", Diagnosis="Респираторные инфекции", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 800, Dateofvisit=DateTime.Parse("2021-05-21"), Duration="35 мин",},
            new Visit{AnimalID=5,OwnerID=4,Complaints="Диарея", Diagnosis="Непереносимость компонентов пищи, отдельных продуктов, аллергия", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 300, Dateofvisit=DateTime.Parse("2021-05-22"), Duration="7 мин" },
            new Visit{AnimalID=6,OwnerID=5,Complaints="Похудел", Diagnosis="Дефицит витаминов ", AttendinDoctor="Эндокринолог Волков Семен Романович", Price=1200 , Dateofvisit=DateTime.Parse("2021-05-21"), Duration="17 мин" },
            new Visit{AnimalID=7,OwnerID=5,Complaints="Жажда", Diagnosis="Диабет", AttendinDoctor="Эндокринолог Волков Семен Романович", Price=1200 , Dateofvisit=DateTime.Parse("2021-05-22"), Duration="20 мин" },
            new Visit{AnimalID=8,OwnerID=6,Complaints="Кашель", Diagnosis="Астма", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price=780 , Dateofvisit=DateTime.Parse("2021-05-23"), Duration="45 мин" },
            new Visit{AnimalID=9,OwnerID=7,Complaints="Отеки", Diagnosis="Травмирование тканей", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price=900 , Dateofvisit=DateTime.Parse("2021-05-23"), Duration="31 мин" },
            new Visit{AnimalID=10,OwnerID=8,Complaints="Неестественная поза", Diagnosis="Столбняк", AttendinDoctor="Невролог Синицына Анна Александровна", Price= 600, Dateofvisit=DateTime.Parse("2021-05-22"), Duration="25 мин" },
            new Visit{AnimalID=11,OwnerID=9,Complaints="Надутый живот", Diagnosis="Абсцесс", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 1600, Dateofvisit=DateTime.Parse("2021-05-21"), Duration="60 мин" },
            new Visit{AnimalID=12,OwnerID=10,Complaints="Синюшность слизистых", Diagnosis="Пневмоторакс", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price=500 , Dateofvisit=DateTime.Parse("2021-05-24"), Duration="14 мин" },
            new Visit{AnimalID=13,OwnerID=11,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price=2000 , Dateofvisit=DateTime.Parse("2021-05-22"), Duration="1 час 30 мин" },
            new Visit{AnimalID=14,OwnerID=12,Complaints="Проглотил инородный предмет", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 3000, Dateofvisit=DateTime.Parse("2021-05-25"), Duration="2 часа 15 мин" },
            new Visit{AnimalID=15,OwnerID=13,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 2000, Dateofvisit=DateTime.Parse("2021-05-24"), Duration="1 час 30 мин" },
            new Visit{AnimalID=16,OwnerID=14,Complaints="Чешется", Diagnosis="Чесоточный клещ", AttendinDoctor="Дерматолог Савина Марина Валерьевна", Price= 820, Dateofvisit=DateTime.Parse("2021-05-21"), Duration="8 мин" },
            new Visit{AnimalID=17,OwnerID=15,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 2000, Dateofvisit=DateTime.Parse("2021-05-24"), Duration="1 час 30 мин" },
            new Visit{AnimalID=18,OwnerID=16,Complaints="Тяжело сглатывает", Diagnosis="Воспаление глотки", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 450, Dateofvisit=DateTime.Parse("2021-05-25"), Duration="12 мин" },
            new Visit{AnimalID=19,OwnerID=17,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 2000, Dateofvisit=DateTime.Parse("2021-05-24"), Duration="1 час 30 мин" },
            new Visit{AnimalID=20,OwnerID=18,Complaints="Продолжительная рвота", Diagnosis="Заболеваний почек", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 600, Dateofvisit=DateTime.Parse("2021-05-25"), Duration="16 мин" },
            new Visit{AnimalID=21,OwnerID=19,Complaints="Тяжело сглатывает", Diagnosis="Опухоль в гортани", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 1600, Dateofvisit=DateTime.Parse("2021-05-26"), Duration="1 час 45 мин" },
            new Visit{AnimalID=22,OwnerID=20,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price=2000 , Dateofvisit=DateTime.Parse("2021-05-24"), Duration="1 час 30 мин" },
            new Visit{AnimalID=23,OwnerID=21,Complaints="Вылизывает одно и то же место", Diagnosis=" Грибок, вызывающий дерматомикоз", AttendinDoctor="Дерматолог Савина Марина Валерьевна", Price= 580, Dateofvisit=DateTime.Parse("2021-05-23"), Duration="17 мин" },
            new Visit{AnimalID=24,OwnerID=22,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 2000, Dateofvisit=DateTime.Parse("2021-05-24"), Duration="1 час 30 мин" },
            new Visit{AnimalID=25,OwnerID=23,Complaints="Необычные выделения", Diagnosis="Камни в почках", AttendinDoctor="Терапевт Антонов Анатолий Сергеевич", Price= 750, Dateofvisit=DateTime.Parse("2021-05-26"), Duration="50 мин" },
            new Visit{AnimalID=27,OwnerID=24,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price= 2000, Dateofvisit=DateTime.Parse("2021-05-23"), Duration="1 час 30 мин" },
            new Visit{AnimalID=1,OwnerID=26,Complaints="Много ест за день, не останавливается", Diagnosis="Желудочно-кишечных заболеваниях", AttendinDoctor="Ширыкалова Дарья Витальевна ", Price= 300, Dateofvisit=DateTime.Parse("2021-05-20"), Duration="15 мин" },
            new Visit{AnimalID=28,OwnerID=14,Complaints="Просят Стерилизацию", Diagnosis="--", AttendinDoctor="Хирург Кузьмин Валентин Владимирович", Price=2000 , Dateofvisit=DateTime.Parse("2021-05-23"), Duration="1 час 30 мин" },
            new Visit{AnimalID=29,OwnerID=18,Complaints="Прячется", Diagnosis="Стрресс", AttendinDoctor="Невролог Синицына Анна Александровна", Price=690 , Dateofvisit=DateTime.Parse("2021-05-23"), Duration="25 мин" },
            new Visit{AnimalID=30,OwnerID=20,Complaints="Волочит лапы", Diagnosis="Грыжа межпозвоночного диска", AttendinDoctor="Невролог Синицына Анна Александровна", Price= 990, Dateofvisit=DateTime.Parse("2021-05-24"), Duration="40 мин" }
            };
            foreach (Visit v in visits)
            {
                context.Visits.Add(v);
            }
            context.SaveChanges();
        }
    }
}

