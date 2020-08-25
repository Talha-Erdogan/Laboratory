Asp.Net Core Mvc - Laboratuvar Uygulaması

* Asp.net Core Mvc (3.1) ile oluşturulmuştur.
* Asp.net Core Web API Mvc (3.1) ile oluşturulmuştur.

Katmanlı mimari yapısı ile proje geliştirilmiştir. Mimari 5 katmandan oluşmaktadır:
  * Laboratory.API
  * Laboratory.API.Business
  * Laboratory.API.Data
  * Laboratory.Web
  * Laboratory.Web.Business

Proje katmanlarından kısaca bahsedecek olursak; 
  - Laboratory.API : Web ve Mobil uygulamalarını besleyecek olan API katmanı kodlanmıştır. Asp.Net Core Mvc Web API (3.1) ile kodlanmıştır.  Web ve Mobil isteklerin tamamı buraya                     yapılacaktır. Http metodlarından "GET,POST,PUT ve DELETE" kullanılmıştır. API haberleşmelerinde güvenlik için "JWT" token kullanılmıştır. JWT token sayesinde                     yetki kontolleri yapılarak, istek atan kullanıcının sadece yetkisi kadar bilgilere erişimi sağlanmaktadır.
  - Laboratory.API.Business : Sql veri tabanı bağlantılarının sağlandığı, isteklerin atıldığı katmandır. Paginated algoritmaları kullanılarak, veri tabanındaki tüm verilerin                         aktarımı yerine sayfalı şekilde aktarılması sağlanmıştır. Ayrıyeten, Laboratory.API katmanının tüm isteklerindeki "ekleme,güncelleme,silme ve listeleme"                           işlevlerinin alt yapısının oluşturulduğu katmandır.
  - Laboratory.API.Data : Sql veri tabanındaki tabloların bulunduğu katmandır. Tablo isimleri ve property'lerini içermektedir. Ayrıyeten Entity Framework ile Sql bağlantılarının                     sağlandığı "DbConnection" bilgilerinin bulunduğu katmandır.
  - Laboratory.Web : Admin işlevlerin yapıldığı katmandır. Asp.Net Core Mvc(3.1) teknolojisi ile kodlanmıştır. Web katmanından yönetim işlevleri ayarlanıp, veri ekleme, silme ,                      düzenleme ve listeleme işlevlerinin yapılması sağlanır. Ayrıyeten kullanıcılara erişim yetkilerinin verilmesi veya silinmesi işlevlerinin ayarlandığı                            katmandır. Kısaca yönetim paneli olarak düşünebiliriz. Buradaki işlevlerden biri de Login olan kişilerin yetkisi dahilinde bulunduğu işlevleri yapabilmesidir.
                   Örneğin, Laboratuvar ekleme, silme ve listeleme profiline sahip kullanıcı sadece kendi yetkisi dahilindeki işlevleri yapabilir. Buradaki işlevler de yetki grupları                    oluşturulup, yetki gruplarına kişiler eklenmektedir. Bu sayede Login olan kişinin yetkisi bulunduğu kısımlara erişimi sağlanmaktadır.
  - Laboratory.Web.Business : Web katmanının beslendiği katmandır. Session bilgisinin tutulduğu ve API'ye istekler sırasında Token bilgisi ile erişimin sağlandığı katmandır.                          HttpClient ile API'ye erişim sağlanmaktadır. "Captcha" nın oluşturulması ve sessionda bunun kontrolleri sağlanmaktadır.
 
Kullanılan Teknolojiler:
 - Asp.Net Core Mvc(3.1)
 - Asp.Net Core Mvc Web API(3.1)
 - Sql
 - Entity Framework
 - Jwt (Token)
 - Dependency Injection
 - Front-End temas olarak:  Core UI Teması
 - MVC Toplu olarak veri girişi
 - Paginated Algoritmaları (CurrentPage, PageSize, From, To ...)
