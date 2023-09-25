﻿IF EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES 
					WHERE TABLE_NAME = N'Pets')
	AND EXISTS (SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS 
					WHERE COLUMN_NAME = N'Breed')
BEGIN
	BEGIN TRANSACTION 
		UPDATE Pets SET Breed = 
			CASE 
				WHEN Breed is NULL THEN 'other'
				WHEN Breed = 'Riba borac' THEN 'fighterFish' 
				WHEN Breed = 'Ciklidi' THEN 'cichlids' 
				WHEN Breed = 'Ajkula' THEN 'shark' 
				WHEN Breed = 'Rečna' THEN 'river' 
				WHEN Breed = 'Kozica' THEN 'prawn' 
				WHEN Breed = 'Algar' THEN 'algar' 
				WHEN Breed = 'Neonka' THEN 'neon' 
				WHEN Breed = 'Ostalo' THEN 'other' 
				WHEN Breed = 'Sivi' THEN 'gray' 
				WHEN Breed = 'Evropski' THEN 'european' 
				WHEN Breed = 'Zlatni' THEN 'goldenM' 
				WHEN Breed = 'Patuljasti' THEN 'dwarf' 
				WHEN Breed = 'Veliki dugorepi' THEN 'bigLongTail' 
				WHEN Breed = 'Barska' THEN 'coastal' 
				WHEN Breed = 'Rečna' THEN 'river' 
				WHEN Breed = 'Crvenouha' THEN 'redEared' 
				WHEN Breed = 'Morska' THEN 'marine' 
				WHEN Breed = 'Šumska' THEN 'forest' 
				WHEN Breed = 'Ostalo' THEN 'other' 
				WHEN Breed = 'Rozeikolis' THEN 'parrotNum1' 
				WHEN Breed = 'Fišer' THEN 'parrotNum2' 
				WHEN Breed = 'Personata' THEN 'parrotNum3' 
				WHEN Breed = 'Tigrica' THEN 'parrotNum4' 
				WHEN Breed = 'Braonuha konura' THEN 'parrotNum5' 
				WHEN Breed = 'Senegalac' THEN 'parrotNum6' 
				WHEN Breed = 'Ninfa' THEN 'parrotNum7' 
				WHEN Breed = 'Dugina lorika' THEN 'parrotNum8' 
				WHEN Breed = 'Crvenokrili papagaj' THEN 'parrotNum9' 
				WHEN Breed = 'Kraljevski papagaj' THEN 'parrotNum10' 
				WHEN Breed = 'Barband' THEN 'parrotNum11' 
				WHEN Breed = 'Berg' THEN 'parrotNum12' 
				WHEN Breed = 'Princ od Velsa' THEN 'parrotNum13' 
				WHEN Breed = 'Crvenooki papagaj' THEN 'parrotNum14' 
				WHEN Breed = 'Bernard' THEN 'parrotNum15' 
				WHEN Breed = 'Penant' THEN 'parrotNum16' 
				WHEN Breed = 'Rozela' THEN 'parrotNum17' 
				WHEN Breed = 'Stenlijeva rozela' THEN 'parrotNum18' 
				WHEN Breed = 'Mali Aleksandar' THEN 'parrotNum19' 
				WHEN Breed = 'Šljivoglavi papagaj' THEN 'parrotNum20' 
				WHEN Breed = 'Bradati Aleksandar' THEN 'parrotNum21' 
				WHEN Breed = 'Zlatnovrata Ara' THEN 'parrotNum22' 
				WHEN Breed = 'Ara Manilata' THEN 'parrotNum23' 
				WHEN Breed = 'Ara Marakana' THEN 'parrotNum24' 
				WHEN Breed = 'Ara Nobilis' THEN 'parrotNum25' 
				WHEN Breed = 'Plavoglava aratinga' THEN 'parrotNum26' 
				WHEN Breed = 'Zlatna konura' THEN 'parrotNum27' 
				WHEN Breed = 'Gvajahi aratinga' THEN 'parrotNum28' 
				WHEN Breed = 'Jandaja' THEN 'parrotNum29' 
				WHEN Breed = 'Zlatokapa aratinga' THEN 'parrotNum30' 
				WHEN Breed = 'Sunčana konura' THEN 'parrotNum31' 
				WHEN Breed = 'Nandaja' THEN 'parrotNum32' 
				WHEN Breed = 'Patagonac' THEN 'parrotNum33' 
				WHEN Breed = 'Maksimilijan' THEN 'parrotNum34' 
				WHEN Breed = 'Beločeli amazonac' THEN 'parrotNum35' 
				WHEN Breed = 'Afrički sivi - Žako' THEN 'parrotNum36' 
				WHEN Breed = 'Roza kakadu' THEN 'parrotNum37' 
				WHEN Breed = 'Mali žutoćubi kakadu' THEN 'parrotNum38' 
				WHEN Breed = 'Veliki žutoćubi kakadu' THEN 'parrotNum39' 
				WHEN Breed = 'Molučki kakadu' THEN 'parrotNum40' 
				WHEN Breed = 'Beloćubi kakadu' THEN 'parrotNum41' 
				WHEN Breed = 'Gofin' THEN 'parrotNum42' 
				WHEN Breed = 'Mala korela' THEN 'parrotNum43' 
				WHEN Breed = 'Kea' THEN 'parrotNum44' 
				WHEN Breed = 'Edel' THEN 'parrotNum45' 
				WHEN Breed = 'Veliki Aleksandar' THEN 'parrotNum46' 
				WHEN Breed = 'Kina Aleksandar' THEN 'parrotNum47' 
				WHEN Breed = 'Hijacintna Ara' THEN 'parrotNum48' 
				WHEN Breed = 'Plavo-Žuta Ara' THEN 'parrotNum49' 
				WHEN Breed = 'Skarletna Ara' THEN 'parrotNum50' 
				WHEN Breed = 'Zelenokrila Ara' THEN 'parrotNum51' 
				WHEN Breed = 'Bufonova Ara' THEN 'parrotNum52' 
				WHEN Breed = 'Vojnička Ara' THEN 'parrotNum53' 
				WHEN Breed = 'Ara severa' THEN 'parrotNum54' 
				WHEN Breed = 'Kuba Amazonac' THEN 'parrotNum55' 
				WHEN Breed = 'Plavočeli Amazonac' THEN 'parrotNum56' 
				WHEN Breed = 'Venecuela Amazonac' THEN 'parrotNum57' 
				WHEN Breed = 'Žutočeli Amazonac' THEN 'parrotNum58' 
				WHEN Breed = 'Milerov Amazonac' THEN 'parrotNum59' 
				WHEN Breed = 'Albino' THEN 'catType1' 
				WHEN Breed = 'Američka žičana dlaka' THEN 'catType2' 
				WHEN Breed = 'Azijska' THEN 'catType3' 
				WHEN Breed = 'Australijska magla' THEN 'catType4' 
				WHEN Breed = 'Balinezijska' THEN 'catType5' 
				WHEN Breed = 'Bengalska' THEN 'catType6' 
				WHEN Breed = 'Burmanska' THEN 'catType7' 
				WHEN Breed = 'Bombajska' THEN 'catType8' 
				WHEN Breed = 'Britanska kratkodlaka' THEN 'catType9' 
				WHEN Breed = 'Burmanska' THEN 'catType10' 
				WHEN Breed = 'Burmila' THEN 'catType11' 
				WHEN Breed = 'Činčila' THEN 'catType12' 
				WHEN Breed = 'Kornišonska kovrčava' THEN 'catType13' 
				WHEN Breed = 'Kimrik' THEN 'catType14' 
				WHEN Breed = 'Devon Reks' THEN 'catType15' 
				WHEN Breed = 'Egipatska Mau' THEN 'catType16' 
				WHEN Breed = 'Egzotična kratkodlaka' THEN 'catType17' 
				WHEN Breed = 'Japanski dugodlaki Bobtail' THEN 'catType18' 
				WHEN Breed = 'Japanski kratkodlaki Bobtail' THEN 'catType19' 
				WHEN Breed = 'Khao Manee' THEN 'catType20' 
				WHEN Breed = 'Korat' THEN 'catType21' 
				WHEN Breed = 'La Perm' THEN 'catType22' 
				WHEN Breed = 'Meinkun' THEN 'catType23' 
				WHEN Breed = 'Manks' THEN 'catType24' 
				WHEN Breed = 'Munchkin' THEN 'catType25' 
				WHEN Breed = 'Norveška šumska' THEN 'catType26' 
				WHEN Breed = 'Ociket' THEN 'catType27' 
				WHEN Breed = 'Orijentalna dugodlaka' THEN 'catType28' 
				WHEN Breed = 'Orijentalna kratkodlaka' THEN 'catType29' 
				WHEN Breed = 'Persijska' THEN 'catType30' 
				WHEN Breed = 'Pixie-bob' THEN 'catType31' 
				WHEN Breed = 'Ragdoll' THEN 'catType32' 
				WHEN Breed = 'Ruska plava' THEN 'catType33' 
				WHEN Breed = 'Savana' THEN 'catType34' 
				WHEN Breed = 'Škotski Fold' THEN 'catType35' 
				WHEN Breed = 'Selkirk Rex' THEN 'catType36' 
				WHEN Breed = 'Sijamska' THEN 'catType37' 
				WHEN Breed = 'Sibirska' THEN 'catType38' 
				WHEN Breed = 'Singapurska' THEN 'catType39' 
				WHEN Breed = 'Snowshoe' THEN 'catType40' 
				WHEN Breed = 'Somalijska' THEN 'catType41' 
				WHEN Breed = 'Sfinks' THEN 'catType42' 
				WHEN Breed = 'Tiffanie' THEN 'catType43' 
				WHEN Breed = 'Tonkinese' THEN 'catType44' 
				WHEN Breed = 'Turska Van' THEN 'catType45' 
				WHEN Breed = 'Avganistanski hrt' THEN 'dogType1' 
				WHEN Breed = 'Azavak' THEN 'dogType2' 
				WHEN Breed = 'Aireski ovčar' THEN 'dogType3' 
				WHEN Breed = 'Akita' THEN 'dogType4' 
				WHEN Breed = 'Alentejski pastirski pas' THEN 'dogType5' 
				WHEN Breed = 'Alpski brak jazavičar' THEN 'dogType6' 
				WHEN Breed = 'Aljaski malamut' THEN 'dogType7' 
				WHEN Breed = 'Aljaski haski' THEN 'dogType8' 
				WHEN Breed = 'Američki buldog' THEN 'dogType9' 
				WHEN Breed = 'Američki koker španijel' THEN 'dogType10' 
				WHEN Breed = 'Američki lisičar' THEN 'dogType11' 
				WHEN Breed = 'Američki pit bul terijer' THEN 'dogType12' 
				WHEN Breed = 'Američki stafordski terijer' THEN 'dogType13' 
				WHEN Breed = 'Američki španijel za vodu' THEN 'dogType14' 
				WHEN Breed = 'Anadolski pastirski pas' THEN 'dogType15' 
				WHEN Breed = 'Arapski hrt - slugi' THEN 'dogType16' 
				WHEN Breed = 'Argentinski pas' THEN 'dogType17' 
				WHEN Breed = 'Ardenski govedar' THEN 'dogType18' 
				WHEN Breed = 'Arieški gonič' THEN 'dogType19' 
				WHEN Breed = 'Ariješki ptičar' THEN 'dogType20' 
				WHEN Breed = 'Artezijsko normanski baset' THEN 'dogType21' 
				WHEN Breed = 'Artoaški gonič' THEN 'dogType22' 
				WHEN Breed = 'Atlaski pastirski pas - aidi' THEN 'dogType23' 
				WHEN Breed = 'Australijski govedar' THEN 'dogType24' 
				WHEN Breed = 'Australijski kelpi' THEN 'dogType25' 
				WHEN Breed = 'Australijski ovčar' THEN 'dogType26' 
				WHEN Breed = 'Australijski terijer' THEN 'dogType27' 
				WHEN Breed = 'Austrijski pinč' THEN 'dogType28' 
				WHEN Breed = 'Austrijski ravnodlaki gonič' THEN 'dogType29' 
				WHEN Breed = 'Bavarski planinski krvoslednik' THEN 'dogType30' 
				WHEN Breed = 'Barak' THEN 'dogType31' 
				WHEN Breed = 'Barbe' THEN 'dogType32' 
				WHEN Breed = 'Basendži' THEN 'dogType33' 
				WHEN Breed = 'Baset' THEN 'dogType34' 
				WHEN Breed = 'Bedlington terijer' THEN 'dogType35' 
				WHEN Breed = 'Belgijski grifon' THEN 'dogType36' 
				WHEN Breed = 'Belgijski ovčar' THEN 'dogType37' 
				WHEN Breed = 'Grenendal' THEN 'dogType38' 
				WHEN Breed = 'Lakenoa' THEN 'dogType39' 
				WHEN Breed = 'Malinoa' THEN 'dogType40' 
				WHEN Breed = 'Beli švajcarski ovčar' THEN 'dogType41' 
				WHEN Breed = 'Bergamski pastirski pas' THEN 'dogType42' 
				WHEN Breed = 'Bernardinac' THEN 'dogType43' 
				WHEN Breed = 'Bernski pastirski pas' THEN 'dogType44' 
				WHEN Breed = 'Bigl zečar' THEN 'dogType45' 
				WHEN Breed = 'Bigl' THEN 'dogType46' 
				WHEN Breed = 'Bojkin španijel' THEN 'dogType47' 
				WHEN Breed = 'Bokser' THEN 'dogType48' 
				WHEN Breed = 'Bolonjski pas' THEN 'dogType49' 
				WHEN Breed = 'Border koli' THEN 'dogType50' 
				WHEN Breed = 'Border terijer' THEN 'dogType51' 
				WHEN Breed = 'Bordoška doga' THEN 'dogType52' 
				WHEN Breed = 'Boseron' THEN 'dogType53' 
				WHEN Breed = 'Bostonski terijer' THEN 'dogType54' 
				WHEN Breed = 'Bradati koli' THEN 'dogType55' 
				WHEN Breed = 'Brazilski pas' THEN 'dogType56' 
				WHEN Breed = 'Brazilski terijer' THEN 'dogType57' 
				WHEN Breed = 'Bretanjski riđi baset' THEN 'dogType58' 
				WHEN Breed = 'Briješki ovčar' THEN 'dogType59' 
				WHEN Breed = 'Briselski grifon' THEN 'dogType60' 
				WHEN Breed = 'Bul terijer' THEN 'dogType61' 
				WHEN Breed = 'Buldog' THEN 'dogType62' 
				WHEN Breed = 'Bulmastif' THEN 'dogType63' 
				WHEN Breed = 'Burbonski ptičar' THEN 'dogType64' 
				WHEN Breed = 'Burgoški jarebičar' THEN 'dogType65' 
				WHEN Breed = 'Vajmarski ptičar' THEN 'dogType66' 
				WHEN Breed = 'Veliki vendejski grifon' THEN 'dogType67' 
				WHEN Breed = 'Veliki gaskonjsko sentanžujski gonič' THEN 'dogType68' 
				WHEN Breed = 'Veliki engleski hrt - grejhund' THEN 'dogType69' 
				WHEN Breed = 'Veliki englesko - francuski belo oranž gonič' THEN 'dogType70' 
				WHEN Breed = 'Veliki englesko - francuski belo crni gonič' THEN 'dogType71' 
				WHEN Breed = 'Veliki englesko - francuski gonič' THEN 'dogType72' 
				WHEN Breed = 'Veliki minsterlendski ptičar' THEN 'dogType73' 
				WHEN Breed = 'Veliki oštrodlaki vendejski baset' THEN 'dogType74' 
				WHEN Breed = 'Veliki plavi gaskonjski gonič' THEN 'dogType75' 
				WHEN Breed = 'Veliki švajcarski pastirski pas' THEN 'dogType76' 
				WHEN Breed = 'Veliki šnaucer' THEN 'dogType77' 
				WHEN Breed = 'Velški korgi kardigan' THEN 'dogType78' 
				WHEN Breed = 'Velški korgi pembrok' THEN 'dogType79' 
				WHEN Breed = 'Velški terijer' THEN 'dogType80' 
				WHEN Breed = 'Velški špringer španijel' THEN 'dogType81' 
				WHEN Breed = 'Vendejski oštrodlaki gonič' THEN 'dogType82' 
				WHEN Breed = 'Vestfalski brak jazavičar' THEN 'dogType83' 
				WHEN Breed = 'Gonič rakuna' THEN 'dogType84' 
				WHEN Breed = 'Gordon seter' THEN 'dogType85' 
				WHEN Breed = 'Grenlandski pas' THEN 'dogType86' 
				WHEN Breed = 'Grčki gonič' THEN 'dogType87' 
				WHEN Breed = 'Dalmatinac' THEN 'dogType88' 
				WHEN Breed = 'Dandi dinmont terijer' THEN 'dogType89' 
				WHEN Breed = 'Danski pas' THEN 'dogType90' 
				WHEN Breed = 'Doberman' THEN 'dogType91' 
				WHEN Breed = 'Drever' THEN 'dogType92' 
				WHEN Breed = 'Drentski ptičar' THEN 'dogType93' 
				WHEN Breed = 'Dugodlaki škotski ovčar' THEN 'dogType94' 
				WHEN Breed = 'Dunkerov gonič' THEN 'dogType95' 
				WHEN Breed = 'Engleski koker španijel' THEN 'dogType96' 
				WHEN Breed = 'Engleski lisičar' THEN 'dogType97' 
				WHEN Breed = 'Engleski mastif' THEN 'dogType98' 
				WHEN Breed = 'Engleski ovčar' THEN 'dogType99' 
				WHEN Breed = 'Engleski pointer' THEN 'dogType100' 
				WHEN Breed = 'Engleski seter' THEN 'dogType101' 
				WHEN Breed = 'Engleski springer španijel' THEN 'dogType102' 
				WHEN Breed = 'Engleski patuljasti terijer' THEN 'dogType103' 
				WHEN Breed = 'Entlebuški pastirski pas' THEN 'dogType104' 
				WHEN Breed = 'Epanjel breton' THEN 'dogType105' 
				WHEN Breed = 'Erdel terijer' THEN 'dogType106' 
				WHEN Breed = 'Erdeljski gonič' THEN 'dogType107' 
				WHEN Breed = 'Estrelski pastirski pas' THEN 'dogType108' 
				WHEN Breed = 'Eurasier' THEN 'dogType109' 
				WHEN Breed = 'Zapadno sibirska lajka' THEN 'dogType110' 
				WHEN Breed = 'Zapadno škotski beli terijer' THEN 'dogType111' 
				WHEN Breed = 'Zlatni retriver' THEN 'dogType112' 
				WHEN Breed = 'Ibizki hrt' THEN 'dogType113' 
				WHEN Breed = 'Izraelski ovčar' THEN 'dogType114' 
				WHEN Breed = 'Imalski terijer' THEN 'dogType115' 
				WHEN Breed = 'Irski vučji hrt' THEN 'dogType116' 
				WHEN Breed = 'Irski seter crveni' THEN 'dogType117' 
				WHEN Breed = 'Irski terijer' THEN 'dogType118' 
				WHEN Breed = 'Irski crveno - beli seter' THEN 'dogType119' 
				WHEN Breed = 'Irski španijel za vodu' THEN 'dogType120' 
				WHEN Breed = 'Islandski ovčar' THEN 'dogType121' 
				WHEN Breed = 'Istarski kratkodlaki gonič' THEN 'dogType122' 
				WHEN Breed = 'Istarski ovčar' THEN 'dogType123' 
				WHEN Breed = 'Istarski oštrodlaki gonič' THEN 'dogType124' 
				WHEN Breed = 'Istočno sibirska lajka' THEN 'dogType125' 
				WHEN Breed = 'Italijanski volpino' THEN 'dogType126' 
				WHEN Breed = 'Italijanski gonič -kratkodlaki' THEN 'dogType127' 
				WHEN Breed = 'Italijanski gonič -oštrodlaki' THEN 'dogType128' 
				WHEN Breed = 'Italijanski kratkodlaki ptičar' THEN 'dogType129' 
				WHEN Breed = 'Italijanski spinon' THEN 'dogType130' 
				WHEN Breed = 'Jazavičar' THEN 'dogType131' 
				WHEN Breed = 'Japanski borac' THEN 'dogType132' 
				WHEN Breed = 'Japanski terijer' THEN 'dogType133' 
				WHEN Breed = 'Japanski čin' THEN 'dogType134' 
				WHEN Breed = 'Japanski špic' THEN 'dogType135' 
				WHEN Breed = 'Jemtlandski pas' THEN 'dogType136' 
				WHEN Breed = 'Jorkširski terijer' THEN 'dogType137' 
				WHEN Breed = 'Južnoruski ovčar' THEN 'dogType138' 
				WHEN Breed = 'Kavalirski španijel kralja Čarlsa' THEN 'dogType139' 
				WHEN Breed = 'Kavkaski ovčar' THEN 'dogType140' 
				WHEN Breed = 'Kanarski pas' THEN 'dogType141' 
				WHEN Breed = 'Kane korso' THEN 'dogType142' 
				WHEN Breed = 'Karelijski gonič medveda' THEN 'dogType143' 
				WHEN Breed = 'Katalonski ovčar' THEN 'dogType144' 
				WHEN Breed = 'Keri blu terijer' THEN 'dogType145' 
				WHEN Breed = 'King Čarls španijel' THEN 'dogType146' 
				WHEN Breed = 'Klamber španijel' THEN 'dogType147' 
				WHEN Breed = 'Kovrdžavi bišon' THEN 'dogType148' 
				WHEN Breed = 'Komondor' THEN 'dogType149' 
				WHEN Breed = 'Kortalsov grifon' THEN 'dogType150' 
				WHEN Breed = 'Kratkodlaki škotski ovčar' THEN 'dogType151' 
				WHEN Breed = 'Kromfolender' THEN 'dogType152' 
				WHEN Breed = 'Kuvas' THEN 'dogType153' 
				WHEN Breed = 'Labrador retriver' THEN 'dogType154' 
				WHEN Breed = 'Lagoto romanjolo' THEN 'dogType155' 
				WHEN Breed = 'Landsir' THEN 'dogType156' 
				WHEN Breed = 'Laponski pas' THEN 'dogType157' 
				WHEN Breed = 'Laponski pastirski pas' THEN 'dogType158' 
				WHEN Breed = 'Laponski špic' THEN 'dogType159' 
				WHEN Breed = 'Lasa apso' THEN 'dogType160' 
				WHEN Breed = 'Lejklandski terijer' THEN 'dogType161' 
				WHEN Breed = 'Leonberger' THEN 'dogType162' 
				WHEN Breed = 'Mađarska vižla kratkodlaka' THEN 'dogType163' 
				WHEN Breed = 'Mađarska vižla oštrodlaka' THEN 'dogType164' 
				WHEN Breed = 'Mađarski agar' THEN 'dogType165' 
				WHEN Breed = 'Majmunski pinč' THEN 'dogType166' 
				WHEN Breed = 'Majorški čuvar' THEN 'dogType167' 
				WHEN Breed = 'Malamut' THEN 'dogType168' 
				WHEN Breed = 'Mali engleski hrt' THEN 'dogType169' 
				WHEN Breed = 'Mali englesko-francuski mat gonič' THEN 'dogType170' 
				WHEN Breed = 'Mali italijanski hrt' THEN 'dogType171' 
				WHEN Breed = 'Mali lavlji pas' THEN 'dogType172' 
				WHEN Breed = 'Mali minsterlender' THEN 'dogType173' 
				WHEN Breed = 'Mali oštrodlaki Vendejski baset' THEN 'dogType174' 
				WHEN Breed = 'Mali holandski pas za lov ptica na vodi' THEN 'dogType175' 
				WHEN Breed = 'Maltezer' THEN 'dogType176' 
				WHEN Breed = 'Mančester terijer toj' THEN 'dogType177' 
				WHEN Breed = 'Mančester terijer' THEN 'dogType178' 
				WHEN Breed = 'Maremano Abruceški pastirski pas' THEN 'dogType179' 
				WHEN Breed = 'Mekodlaki pšenično-žuti terijer' THEN 'dogType180' 
				WHEN Breed = 'Meksički golokoži pas' THEN 'dogType181' 
				WHEN Breed = 'Minijaturni bul terijer' THEN 'dogType182' 
				WHEN Breed = 'Mops' THEN 'dogType183' 
				WHEN Breed = 'Mudi' THEN 'dogType184' 
				WHEN Breed = 'Nemačka doga' THEN 'dogType185' 
				WHEN Breed = 'Nemački gonič' THEN 'dogType186' 
				WHEN Breed = 'Nemački dugodlaki ptičar' THEN 'dogType187' 
				WHEN Breed = 'Nemački kratkodlaki ptičar' THEN 'dogType188' 
				WHEN Breed = 'Nemački lovni terijer' THEN 'dogType189' 
				WHEN Breed = 'Nemački prepeličar' THEN 'dogType190' 
				WHEN Breed = 'Nemački ovčar' THEN 'dogType191' 
				WHEN Breed = 'Nemački oštrodlaki ptičar' THEN 'dogType192' 
				WHEN Breed = 'Nemački oštrodlaki ptičar' THEN 'dogType193' 
				WHEN Breed = 'Nemački špic' THEN 'dogType194' 
				WHEN Breed = 'Nivernejski oštrodlaki gonič' THEN 'dogType195' 
				WHEN Breed = 'Njufaundlendski pas' THEN 'dogType196' 
				WHEN Breed = 'Overnejski ptičar' THEN 'dogType197' 
				WHEN Breed = 'Pas svetog Huberta - blodhund' THEN 'dogType198' 
				WHEN Breed = 'Pastirski pas iz Laboreira' THEN 'dogType199' 
				WHEN Breed = 'Patuljasti kontinental epanjel' THEN 'dogType200' 
				WHEN Breed = 'Patuljasti pinč' THEN 'dogType201' 
				WHEN Breed = 'Patuljasti šnaucer' THEN 'dogType202' 
				WHEN Breed = 'Pekinezer' THEN 'dogType203' 
				WHEN Breed = 'Pikardijski epanjel' THEN 'dogType204' 
				WHEN Breed = 'Pikardijski ovčar' THEN 'dogType205' 
				WHEN Breed = 'Pinč' THEN 'dogType206' 
				WHEN Breed = 'Pirinejski mastif' THEN 'dogType207' 
				WHEN Breed = 'Pirinejski ovčar duge dlake' THEN 'dogType208' 
				WHEN Breed = 'Pirinejski ovčar kratke dlake' THEN 'dogType209' 
				WHEN Breed = 'Pirinejski planinski pas' THEN 'dogType210' 
				WHEN Breed = 'Plavi gaskonjski gonič' THEN 'dogType211' 
				WHEN Breed = 'Plavi oštrodlaki gaskonjski gonič' THEN 'dogType212' 
				WHEN Breed = 'Plavi pikardijski epanjel' THEN 'dogType213' 
				WHEN Breed = 'Podhalanski ovčar' THEN 'dogType214' 
				WHEN Breed = 'Poljski ravničarski ovčar' THEN 'dogType215' 
				WHEN Breed = 'Poljski španijel' THEN 'dogType216' 
				WHEN Breed = 'Pont Odmerški epanjel' THEN 'dogType217' 
				WHEN Breed = 'Portugalski jarebičar' THEN 'dogType218' 
				WHEN Breed = 'Portugalski pas za vodu' THEN 'dogType219' 
				WHEN Breed = 'Posavski gonič' THEN 'dogType220' 
				WHEN Breed = 'Pudla' THEN 'dogType221' 
				WHEN Breed = 'Puli' THEN 'dogType222' 
				WHEN Breed = 'Pulin' THEN 'dogType223' 
				WHEN Breed = 'Pumi' THEN 'dogType224' 
				WHEN Breed = 'Ravnodlaki retriver' THEN 'dogType225' 
				WHEN Breed = 'Rat terijer' THEN 'dogType226' 
				WHEN Breed = 'Riđi bretanjski gonič' THEN 'dogType227' 
				WHEN Breed = 'Rodezijski ridžbek' THEN 'dogType228' 
				WHEN Breed = 'Rotvajler' THEN 'dogType229' 
				WHEN Breed = 'Ruski toj terijer' THEN 'dogType230' 
				WHEN Breed = 'Ruski hrt - Barzoj' THEN 'dogType231' 
				WHEN Breed = 'Rusko - evropska lajka' THEN 'dogType232' 
				WHEN Breed = 'Saluki' THEN 'dogType233' 
				WHEN Breed = 'Samojed' THEN 'dogType234' 
				WHEN Breed = 'Sarloški vučji pas' THEN 'dogType235' 
				WHEN Breed = 'Saseks španijel' THEN 'dogType236' 
				WHEN Breed = 'Senžermenski ptičar' THEN 'dogType237' 
				WHEN Breed = 'Sibirski haski' THEN 'dogType238' 
				WHEN Breed = 'Silihem terijer' THEN 'dogType239' 
				WHEN Breed = 'Silki terijer' THEN 'dogType240' 
				WHEN Breed = 'Skaj terijer' THEN 'dogType241' 
				WHEN Breed = 'Slovački gonič' THEN 'dogType242' 
				WHEN Breed = 'Slovački pastirski pas' THEN 'dogType243' 
				WHEN Breed = 'Smolondski gonič' THEN 'dogType244' 
				WHEN Breed = 'Srednjoazijski ovčar' THEN 'dogType245' 
				WHEN Breed = 'Srpski gonič' THEN 'dogType246' 
				WHEN Breed = 'Srpski odbrambeni pas' THEN 'dogType247' 
				WHEN Breed = 'Srpski trobojni gonič' THEN 'dogType248' 
				WHEN Breed = 'Stari engleski ovčar - bobtejl' THEN 'dogType249' 
				WHEN Breed = 'Stafordski bul terijer' THEN 'dogType250' 
				WHEN Breed = 'Tajlandski ridžbek' THEN 'dogType251' 
				WHEN Breed = 'Tibetski mastif' THEN 'dogType252' 
				WHEN Breed = 'Tibetski terijer' THEN 'dogType253' 
				WHEN Breed = 'Tibetski španijel' THEN 'dogType254' 
				WHEN Breed = 'Tirolski gonič' THEN 'dogType255' 
				WHEN Breed = 'Toling retriver' THEN 'dogType256' 
				WHEN Breed = 'Tornjak' THEN 'dogType257' 
				WHEN Breed = 'Tulearski pas' THEN 'dogType258' 
				WHEN Breed = 'Faraonski pas' THEN 'dogType259' 
				WHEN Breed = 'Finski gonič' THEN 'dogType260' 
				WHEN Breed = 'Finski špic' THEN 'dogType261' 
				WHEN Breed = 'Flandrijski govedar' THEN 'dogType262' 
				WHEN Breed = 'Foks terijer' THEN 'dogType263' 
				WHEN Breed = 'Foks terijer kratkodlaki' THEN 'dogType264' 
				WHEN Breed = 'Foks terijer minijaturni' THEN 'dogType265' 
				WHEN Breed = 'Foks terijer oštrodlaki' THEN 'dogType266' 
				WHEN Breed = 'Foks terijer toj' THEN 'dogType267' 
				WHEN Breed = 'Francuski belo - oranž gonič' THEN 'dogType268' 
				WHEN Breed = 'Francuski belo - crni gonič' THEN 'dogType269' 
				WHEN Breed = 'Francuski buldog' THEN 'dogType270' 
				WHEN Breed = 'Francuski epanjel' THEN 'dogType271' 
				WHEN Breed = 'Francuski ptičar gaskonjski tip' THEN 'dogType272' 
				WHEN Breed = 'Francuski ptičar pirinejski tip' THEN 'dogType273' 
				WHEN Breed = 'Francuski trobojni gonič' THEN 'dogType274' 
				WHEN Breed = 'Frizijski ptičar' THEN 'dogType275' 
				WHEN Breed = 'Frizijski španijel' THEN 'dogType276' 
				WHEN Breed = 'Havanski bišon' THEN 'dogType277' 
				WHEN Breed = 'Haski' THEN 'dogType278' 
				WHEN Breed = 'Hamiltonov gonič' THEN 'dogType279' 
				WHEN Breed = 'Hanoverski krvoslednik' THEN 'dogType280' 
				WHEN Breed = 'Harijer' THEN 'dogType281' 
				WHEN Breed = 'Higenov gonič' THEN 'dogType282' 
				WHEN Breed = 'Hovavart' THEN 'dogType283' 
				WHEN Breed = 'Hokaido' THEN 'dogType284' 
				WHEN Breed = 'Holandski kovrdžavi ovčar' THEN 'dogType285' 
				WHEN Breed = 'Holandski ovčar' THEN 'dogType286' 
				WHEN Breed = 'Holandski pinč' THEN 'dogType287' 
				WHEN Breed = 'Holdenski gonič' THEN 'dogType288' 
				WHEN Breed = 'Hrvatski ovčar' THEN 'dogType289' 
				WHEN Breed = 'Crni ruski terijer' THEN 'dogType290' 
				WHEN Breed = 'Crnogorski planinski gonič' THEN 'dogType291' 
				WHEN Breed = 'Čau čau' THEN 'dogType292' 
				WHEN Breed = 'Češki fousek' THEN 'dogType293' 
				WHEN Breed = 'Čivava' THEN 'dogType294' 
				WHEN Breed = 'Čizopik retriver' THEN 'dogType295' 
				WHEN Breed = 'Džek Raselov terijer' THEN 'dogType296' 
				WHEN Breed = 'Šar pej' THEN 'dogType297' 
				WHEN Breed = 'Šarplaninac' THEN 'dogType298' 
				WHEN Breed = 'Švajcarski gonič' THEN 'dogType299' 
				WHEN Breed = 'Bernski gonič' THEN 'dogType300' 
				WHEN Breed = 'Jurski gonič' THEN 'dogType301' 
				WHEN Breed = 'Lucernski gonič' THEN 'dogType302' 
				WHEN Breed = 'Švajcarski gonič' THEN 'dogType303' 
				WHEN Breed = 'Niskonogi švajcarski gonič' THEN 'dogType304' 
				WHEN Breed = 'Bernski niskonogi gonič' THEN 'dogType305' 
				WHEN Breed = 'Jurski niskonogi gonič' THEN 'dogType306' 
				WHEN Breed = 'Lucernski niskonogi gonič' THEN 'dogType307' 
				WHEN Breed = 'Švajcarski niskonogi gonič' THEN 'dogType308' 
				WHEN Breed = 'Švedski brak jazavičar' THEN 'dogType309' 
				WHEN Breed = 'Švedski ovčarski špic' THEN 'dogType310' 
				WHEN Breed = 'Šetlandski ovčar' THEN 'dogType311' 
				WHEN Breed = 'Ši cu' THEN 'dogType312' 
				WHEN Breed = 'Šiba' THEN 'dogType313' 
				WHEN Breed = 'Šikoku' THEN 'dogType314' 
				WHEN Breed = 'Šilerov gonič' THEN 'dogType315' 
				WHEN Breed = 'Šipeki' THEN 'dogType316' 
				WHEN Breed = 'Škotski jelenji hrt' THEN 'dogType317' 
				WHEN Breed = 'Škotski terijer' THEN 'dogType318' 
				WHEN Breed = 'Šnaucer' THEN 'dogType319' 
				WHEN Breed = 'Španski brdski gonič' THEN 'dogType320' 
				WHEN Breed = 'Španski mastif' THEN 'dogType321' 
				WHEN Breed = 'Španski pas za vodu' THEN 'dogType322' 
				WHEN Breed = 'Štajerski oštrodlaki gonič' THEN 'dogType323' 
				WHEN Breed = 'Ostalo' THEN 'dogType324' 
			ELSE Breed
		END
	COMMIT TRANSACTION
END